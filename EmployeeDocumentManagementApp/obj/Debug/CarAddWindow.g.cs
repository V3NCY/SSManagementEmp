﻿#pragma checksum "..\..\CarAddWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A338AC753C9D64F680239B39B844089336821C95C34AE4C2A9B14B5F4659C5E9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using EmployeeDocumentManagementApp;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace EmployeeDocumentManagementApp {
    
    
    /// <summary>
    /// CarAddWindow
    /// </summary>
    public partial class CarAddWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtBrand;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtModel;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtYearManufacture;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCarNo;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtExpense;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtAcceleration;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEngineType;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtTransmission;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\CarAddWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSuspension;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/EmployeeDocumentManagementApp;component/caraddwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\CarAddWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.txtBrand = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.txtModel = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.txtYearManufacture = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.txtCarNo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.txtExpense = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtAcceleration = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtEngineType = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.txtTransmission = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.txtSuspension = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            
            #line 57 "..\..\CarAddWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnCarAddButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
