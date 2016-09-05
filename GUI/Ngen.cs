using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using Telerik.WinControls.UI;
using Telerik.WinForms;
using Telerik.Windows;
using Telerik.Collections;
using System.Text.RegularExpressions;
using System.Xml;
using BIS.Business;
using BIS.Model;
using System.Configuration;
using Telerik.WinControls;
using BIS.Core;
using System.Reflection;
using System.Resources;
using Telerik.WinControls.UI.Localization;

/// <summary>
/// This is run as a custom action by an MSI installer.
/// It is used to call NGEN, which optimizes the assembly.
/// </summary>
[RunInstaller(true)]
public partial class NgenInstaller : Installer 
{
    public NgenInstaller()
    {
       InitializeComponent();       
        
    }

    /// <summary>
    /// An override function that is called when a MSI installer is run
    /// and the executable is set as a custom action to be run at install time.
    /// The native image generator (NGEN.exe) is called, which provides a
    /// startup performance boost on Windows Forms programs. It is helpful to
    /// comment out the NGEN code and see how the performance
    /// is affected.
    /// </summary>
    /// <param name="stateSaver">No need to change this.</param>
    public override void Install(IDictionary stateSaver)
    {
        base.Install(stateSaver);

        // get the .NET runtime string, and add the ngen exe at the end.
        string runtimeStr = RuntimeEnvironment.GetRuntimeDirectory();
        string ngenStr = Path.Combine(runtimeStr, "ngen.exe");

        // create a new process...
        Process process = new Process();
        process.StartInfo.FileName = ngenStr;

        // get the assembly (exe) path and filename.
        string assemblyPath = Context.Parameters["assemblypath"];

        // add the argument to the filename as the assembly path.
        // Use quotes--important if there are spaces in the name.
        // Use the "install" verb and ngen.exe will compile all deps.
        process.StartInfo.Arguments = "install \"" + assemblyPath + "\"";

        // start ngen. it will do its magic.
        process.Start();
        process.WaitForExit();
    }

    public void InitializeComponent()
    {


    }
}