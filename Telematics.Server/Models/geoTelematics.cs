﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34003
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.0.30319.33440.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class GEOMain {
    
    private GEOMainPointTable pointTableField;
    
    private System.DateTime sendTimeField;
    
    /// <remarks/>
    public GEOMainPointTable PointTable {
        get {
            return this.pointTableField;
        }
        set {
            this.pointTableField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public System.DateTime sendTime {
        get {
            return this.sendTimeField;
        }
        set {
            this.sendTimeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GEOMainPointTable
{
    
    private GEOMainPointTablePoint[] pointField;
    
    private int userIDField;
    
    private string deviceIDField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("Point")]
    public GEOMainPointTablePoint[] Point {
        get {
            return this.pointField;
        }
        set {
            this.pointField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public int UserID {
        get {
            return this.userIDField;
        }
        set {
            this.userIDField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string DeviceID {
        get {
            return this.deviceIDField;
        }
        set {
            this.deviceIDField = value;
        }
    }

}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class GEOMainPointTablePoint
{
    
    private decimal lonField;
    
    private decimal latField;
    
    private byte speedField;
    
    private string uTCTimeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Lon {
        get {
            return this.lonField;
        }
        set {
            this.lonField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal Lat {
        get {
            return this.latField;
        }
        set {
            this.latField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte Speed {
        get {
            return this.speedField;
        }
        set {
            this.speedField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string UTCTime {
        get {
            return this.uTCTimeField;
        }
        set {
            this.uTCTimeField = value;
        }
    }
}
