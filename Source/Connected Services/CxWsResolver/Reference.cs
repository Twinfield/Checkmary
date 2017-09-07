﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Checkmary.CxWsResolver {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CxClientType", Namespace="http://Checkmarx.com")]
    public enum CxClientType : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        WebPortal = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CLI = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Eclipse = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        VS = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        InteliJ = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Audit = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        SDK = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Jenkins = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TFSBuild = 9,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Importer = 10,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Other = 11,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sonar = 12,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CxWSBasicRepsonse", Namespace="http://Checkmarx.com")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Checkmary.CxWsResolver.CxWSResponseDiscovery))]
    public partial class CxWSBasicRepsonse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private bool IsSuccesfullField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public bool IsSuccesfull {
            get {
                return this.IsSuccesfullField;
            }
            set {
                if ((this.IsSuccesfullField.Equals(value) != true)) {
                    this.IsSuccesfullField = value;
                    this.RaisePropertyChanged("IsSuccesfull");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CxWSResponseDiscovery", Namespace="http://Checkmarx.com")]
    [System.SerializableAttribute()]
    public partial class CxWSResponseDiscovery : Checkmary.CxWsResolver.CxWSBasicRepsonse {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServiceURLField;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string ServiceURL {
            get {
                return this.ServiceURLField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceURLField, value) != true)) {
                    this.ServiceURLField = value;
                    this.RaisePropertyChanged("ServiceURL");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Checkmarx.com", ConfigurationName="CxWsResolver.CxWSResolverSoap")]
    public interface CxWSResolverSoap {
        
        // CODEGEN: Generating message contract since element name GetWebServiceUrlResult from namespace http://Checkmarx.com is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://Checkmarx.com/GetWebServiceUrl", ReplyAction="*")]
        Checkmary.CxWsResolver.GetWebServiceUrlResponse GetWebServiceUrl(Checkmary.CxWsResolver.GetWebServiceUrlRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Checkmarx.com/GetWebServiceUrl", ReplyAction="*")]
        System.Threading.Tasks.Task<Checkmary.CxWsResolver.GetWebServiceUrlResponse> GetWebServiceUrlAsync(Checkmary.CxWsResolver.GetWebServiceUrlRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetWebServiceUrlRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetWebServiceUrl", Namespace="http://Checkmarx.com", Order=0)]
        public Checkmary.CxWsResolver.GetWebServiceUrlRequestBody Body;
        
        public GetWebServiceUrlRequest() {
        }
        
        public GetWebServiceUrlRequest(Checkmary.CxWsResolver.GetWebServiceUrlRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://Checkmarx.com")]
    public partial class GetWebServiceUrlRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public Checkmary.CxWsResolver.CxClientType ClientType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int APIVersion;
        
        public GetWebServiceUrlRequestBody() {
        }
        
        public GetWebServiceUrlRequestBody(Checkmary.CxWsResolver.CxClientType ClientType, int APIVersion) {
            this.ClientType = ClientType;
            this.APIVersion = APIVersion;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetWebServiceUrlResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetWebServiceUrlResponse", Namespace="http://Checkmarx.com", Order=0)]
        public Checkmary.CxWsResolver.GetWebServiceUrlResponseBody Body;
        
        public GetWebServiceUrlResponse() {
        }
        
        public GetWebServiceUrlResponse(Checkmary.CxWsResolver.GetWebServiceUrlResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://Checkmarx.com")]
    public partial class GetWebServiceUrlResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public Checkmary.CxWsResolver.CxWSResponseDiscovery GetWebServiceUrlResult;
        
        public GetWebServiceUrlResponseBody() {
        }
        
        public GetWebServiceUrlResponseBody(Checkmary.CxWsResolver.CxWSResponseDiscovery GetWebServiceUrlResult) {
            this.GetWebServiceUrlResult = GetWebServiceUrlResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CxWSResolverSoapChannel : Checkmary.CxWsResolver.CxWSResolverSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CxWSResolverSoapClient : System.ServiceModel.ClientBase<Checkmary.CxWsResolver.CxWSResolverSoap>, Checkmary.CxWsResolver.CxWSResolverSoap {
        
        public CxWSResolverSoapClient() {
        }
        
        public CxWSResolverSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CxWSResolverSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CxWSResolverSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CxWSResolverSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Checkmary.CxWsResolver.GetWebServiceUrlResponse Checkmary.CxWsResolver.CxWSResolverSoap.GetWebServiceUrl(Checkmary.CxWsResolver.GetWebServiceUrlRequest request) {
            return base.Channel.GetWebServiceUrl(request);
        }
        
        public Checkmary.CxWsResolver.CxWSResponseDiscovery GetWebServiceUrl(Checkmary.CxWsResolver.CxClientType ClientType, int APIVersion) {
            Checkmary.CxWsResolver.GetWebServiceUrlRequest inValue = new Checkmary.CxWsResolver.GetWebServiceUrlRequest();
            inValue.Body = new Checkmary.CxWsResolver.GetWebServiceUrlRequestBody();
            inValue.Body.ClientType = ClientType;
            inValue.Body.APIVersion = APIVersion;
            Checkmary.CxWsResolver.GetWebServiceUrlResponse retVal = ((Checkmary.CxWsResolver.CxWSResolverSoap)(this)).GetWebServiceUrl(inValue);
            return retVal.Body.GetWebServiceUrlResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Checkmary.CxWsResolver.GetWebServiceUrlResponse> Checkmary.CxWsResolver.CxWSResolverSoap.GetWebServiceUrlAsync(Checkmary.CxWsResolver.GetWebServiceUrlRequest request) {
            return base.Channel.GetWebServiceUrlAsync(request);
        }
        
        public System.Threading.Tasks.Task<Checkmary.CxWsResolver.GetWebServiceUrlResponse> GetWebServiceUrlAsync(Checkmary.CxWsResolver.CxClientType ClientType, int APIVersion) {
            Checkmary.CxWsResolver.GetWebServiceUrlRequest inValue = new Checkmary.CxWsResolver.GetWebServiceUrlRequest();
            inValue.Body = new Checkmary.CxWsResolver.GetWebServiceUrlRequestBody();
            inValue.Body.ClientType = ClientType;
            inValue.Body.APIVersion = APIVersion;
            return ((Checkmary.CxWsResolver.CxWSResolverSoap)(this)).GetWebServiceUrlAsync(inValue);
        }
    }
}
