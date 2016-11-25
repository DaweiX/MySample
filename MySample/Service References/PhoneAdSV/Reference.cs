﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.VisualStudio.ServiceReference.Platforms, version 14.0.23107.0
// 
namespace MySample.PhoneAdSV {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://WebXml.com.cn/", ConfigurationName="PhoneAdSV.MobileCodeWSSoap")]
    public interface MobileCodeWSSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WebXml.com.cn/getMobileCodeInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string> getMobileCodeInfoAsync(string mobileCode, string userID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://WebXml.com.cn/getDatabaseInfo", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<string[]> getDatabaseInfoAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface MobileCodeWSSoapChannel : MySample.PhoneAdSV.MobileCodeWSSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MobileCodeWSSoapClient : System.ServiceModel.ClientBase<MySample.PhoneAdSV.MobileCodeWSSoap>, MySample.PhoneAdSV.MobileCodeWSSoap {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public MobileCodeWSSoapClient() : 
                base(MobileCodeWSSoapClient.GetDefaultBinding(), MobileCodeWSSoapClient.GetDefaultEndpointAddress()) {
            this.Endpoint.Name = EndpointConfiguration.MobileCodeWSSoap.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MobileCodeWSSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(MobileCodeWSSoapClient.GetBindingForEndpoint(endpointConfiguration), MobileCodeWSSoapClient.GetEndpointAddress(endpointConfiguration)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MobileCodeWSSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(MobileCodeWSSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress)) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MobileCodeWSSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(MobileCodeWSSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress) {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public MobileCodeWSSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Threading.Tasks.Task<string> getMobileCodeInfoAsync(string mobileCode, string userID) {
            return base.Channel.getMobileCodeInfoAsync(mobileCode, userID);
        }
        
        public System.Threading.Tasks.Task<string[]> getDatabaseInfoAsync() {
            return base.Channel.getDatabaseInfoAsync();
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync() {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.MobileCodeWSSoap)) {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration) {
            if ((endpointConfiguration == EndpointConfiguration.MobileCodeWSSoap)) {
                return new System.ServiceModel.EndpointAddress("http://ws.webxml.com.cn/WebServices/MobileCodeWS.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding() {
            return MobileCodeWSSoapClient.GetBindingForEndpoint(EndpointConfiguration.MobileCodeWSSoap);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress() {
            return MobileCodeWSSoapClient.GetEndpointAddress(EndpointConfiguration.MobileCodeWSSoap);
        }
        
        public enum EndpointConfiguration {
            
            MobileCodeWSSoap,
        }
    }
}
