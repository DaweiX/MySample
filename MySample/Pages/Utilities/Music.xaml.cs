using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Media.Playback;
using Windows.Media.Playlists;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Utilities
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Music : Page
    {
        StorageFolder folder = KnownFolders.MusicLibrary;
        string name = "Sample List";
        NameCollisionOption collop = NameCollisionOption.ReplaceExisting;
        PlaylistFormat format = PlaylistFormat.WindowsMedia;
        public Music()
        {
            this.InitializeComponent();
        }

        private async void MediaPlayer_MediaOpened(MediaPlayer sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                PlayOrPause.IsChecked = true;
            });         
        }

        string[] audioExtensions = new string[] { ".wma", ".mp3", ".mp2", ".aac", ".adt", ".adts", ".m4a", ".wav", ".ape" };
        string[] listExtensions = new string[] { ".wpl", ".zpl" };
        private async void creat_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = CreatFilePicker(audioExtensions);
            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                Playlist playlist = new Playlist();
                foreach (StorageFile file in files)
                {
                    playlist.Files.Add(file);
                }
                if (listname.Text != null && listname.Text.Length != 0) 
                    name = listname.Text;              
                try
                {
                    StorageFile savedFile = await playlist.SaveAsAsync(folder, name, collop, format);
                    Msg(savedFile.Name + "被创建并与" + files.Count + "个文件保存");
                }
                catch (Exception err)
                {
                    Msg(err.Message, "错误");
                }
            }
            else
            {
                Msg("没有文件被选中");
            }
        }

        public static FileOpenPicker CreatFilePicker(string[] extensions)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.MusicLibrary;
            foreach (var item in extensions)
            {
                picker.FileTypeFilter.Add(item);
            }
            return picker;
        }
        Playlist playlist;
        string result = "";
        private async void show_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                MyMedia.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
                MyMedia.MediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            });
            playlist = await PickPlayListAsync();
            if (playlist != null)
            {
                list_result.Items.Clear();
                result = "播放列表中的歌曲：" + playlist.Files.Count.ToString() + "\n";
                a.Text = result;
                foreach (var file in playlist.Files)
                {
                    result = "";
                    MusicProperties pros = await file.Properties.GetMusicPropertiesAsync();
                    result += pros.Title + "\n";
                    result += "专辑: " + pros.Album + "\n";
                    result += "艺术家: " + pros.Artist + "\n";
                    result += "时长: " + pros.Duration.ToString().Split('.')[0];
                    list_result.Items.Add(result);
                }
            }
        }

        async public Task<Playlist> PickPlayListAsync()
        {
            FileOpenPicker picker = CreatFilePicker(listExtensions);
            StorageFile file = await picker.PickSingleFileAsync();
            if (file == null)
            {
                Msg("没有文件被选中");
                return null;
            }
            try
            {
                listname.Text = name = file.DisplayName;
                return await Playlist.LoadAsync(file);
            }
            catch (Exception err)
            {
                Msg(err.Message, "错误");
                return null;
            }
        }

        public async void Msg(string Content, string Title = "提示")
        {
            MessageDialog md = new MessageDialog(Content, Title);
            await md.ShowAsync();
        }

        private async void Play()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
             {
                 MyMedia.MediaPlayer.Play();
             });
        }

        private async void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                if (MyMedia.MediaPlayer.IsLoopingEnabled == false)
                    PlayOrPause.IsChecked = false;
            });
        }

        private async void Pause()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                MyMedia.MediaPlayer.Pause();
            });
        }

        StorageFile song;
        bool isAddorRemove = false;
        private async void list_result_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!isAddorRemove)
            {
                int SongIndex = list_result.SelectedIndex;
                song = playlist.Files[SongIndex];
                if (song != null)
                {
                    var stream = await song.OpenAsync(FileAccessMode.Read);
                    MyMedia.AutoPlay = false;
                    await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                    {
                        MyMedia.MediaPlayer.SetStreamSource(stream);
                    });
                }
            }          
        }

        private async void Repeat_Click(object sender, RoutedEventArgs e)
        {
            if (repeat.IsChecked == true)       //列表循环
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    MyMedia.MediaPlayer.IsLoopingEnabled = false;
                    MyMedia.MediaPlayer.MediaEnded += LoopList;
                });
            }
            else if (repeat.IsChecked == null)  //单曲循环
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    MyMedia.MediaPlayer.IsLoopingEnabled = true;
                    MyMedia.MediaPlayer.MediaEnded -= LoopList;
                });
            }
            else if (repeat.IsChecked == false) //不循环
            {
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    MyMedia.MediaPlayer.IsLoopingEnabled = false;
                    MyMedia.MediaPlayer.MediaEnded -= LoopList;
                });
            }
        }

        private async void LoopList(MediaPlayer sender, object args)
        {
            int SongIndex = playlist.Files.IndexOf(song) + 1;
            SongIndex++;
            if (SongIndex == playlist.Files.Count - 1)
            {
                SongIndex = 0;
            }
            song = playlist.Files[SongIndex];
            if (song != null)
            {
                var stream = await song.OpenAsync(FileAccessMode.Read);
                await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                {
                    MyMedia.MediaPlayer.SetStreamSource(stream);
                    list_result.SelectedIndex = SongIndex;
                    MyMedia.MediaPlayer.Play();
                });
            }
        }

        private async void add_Click(object sender, RoutedEventArgs e)
        {
            isAddorRemove = true;
            FileOpenPicker picker = CreatFilePicker(audioExtensions);
            IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();
            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    playlist.Files.Add(file);
                }
                result = "播放列表中的歌曲：" + playlist.Files.Count.ToString() + "\n";
                a.Text = result;
                list_result.Items.Clear();
                foreach (var file in playlist.Files)
                {
                    result = "";
                    MusicProperties pros = await file.Properties.GetMusicPropertiesAsync();
                    result += pros.Title + "\n";
                    result += "专辑: " + pros.Album + "\n";
                    result += "艺术家: " + pros.Artist + "\n";
                    result += "时长: " + pros.Duration.ToString().Split('.')[0];
                    list_result.Items.Add(result);
                }
                StorageFile savedFile = await playlist.SaveAsAsync(folder, name, collop, format);                
            }
            isAddorRemove = false;
        }

        private async void remove_Click(object sender, RoutedEventArgs e)
        {
            isAddorRemove = true;
            if (remove.IsChecked == true)
            {
                list_result.SelectionMode = SelectionMode.Multiple;
            } 
            else
            {
                for (int i = playlist.Files.Count - 1; i > 0; i--) 
                {
                    if (list_result.SelectedItems.Contains(list_result.Items[i]))
                        playlist.Files.RemoveAt(i);
                }
                result = "播放列表中的歌曲：" + playlist.Files.Count.ToString() + "\n";
                a.Text = result;
                list_result.Items.Clear();
                foreach (var file in playlist.Files)
                {
                    result = "";
                    MusicProperties pros = await file.Properties.GetMusicPropertiesAsync();
                    result += pros.Title + "\n";
                    result += "专辑: " + pros.Album + "\n";
                    result += "艺术家: " + pros.Artist + "\n";
                    result += "时长: " + pros.Duration.ToString().Split('.')[0];
                    list_result.Items.Add(result);
                }
                StorageFile savedFile = await playlist.SaveAsAsync(folder, name, collop, format);
                list_result.SelectionMode = SelectionMode.Single;               
            }
            isAddorRemove = false;
        }

        private void PlayOrPause_Click(object sender, RoutedEventArgs e)
        {
            if (PlayOrPause.IsChecked == true)
                Play();
            else
            {
                Pause();
            }
        }
    }

    public class IconChange : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool? b = (bool?)value;         
            if (b == true) return Symbol.Pause;
            return Symbol.Play;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }


    public class IconChange2 : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var b = (bool?)value;
            if (b == null) return Symbol.RepeatOne;
            else return Symbol.RepeatAll;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
