﻿#pragma checksum "..\..\..\AircraftPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2F08B9901C1482FC671B83F9C38820D3DD88C375"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AviationDb;
using AviationDb.Database;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace AviationDb {
    
    
    /// <summary>
    /// AircraftPage
    /// </summary>
    public partial class AircraftPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\AircraftPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid AircraftTable;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\AircraftPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RefreshButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\AircraftPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RemoveButton;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\AircraftPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ModifyButton;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\AircraftPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button CreateNewButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/homework1;component/aircraftpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AircraftPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.AircraftTable = ((System.Windows.Controls.DataGrid)(target));
            
            #line 13 "..\..\..\AircraftPage.xaml"
            this.AircraftTable.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.AircraftTable_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RefreshButton = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\AircraftPage.xaml"
            this.RefreshButton.Click += new System.Windows.RoutedEventHandler(this.RefreshButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.RemoveButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\AircraftPage.xaml"
            this.RemoveButton.Click += new System.Windows.RoutedEventHandler(this.RemoveButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ModifyButton = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\AircraftPage.xaml"
            this.ModifyButton.Click += new System.Windows.RoutedEventHandler(this.ModifyButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.CreateNewButton = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\AircraftPage.xaml"
            this.CreateNewButton.Click += new System.Windows.RoutedEventHandler(this.CreateNewButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
