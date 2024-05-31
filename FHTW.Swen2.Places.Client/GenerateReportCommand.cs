using FHTW.Swen2.Places.Model;
using iText.Kernel.Geom;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FHTW.Swen2.Places.Client
{
    /// <summary>This class implements a command to generate a report.</summary>
    public class GenerateReportCommand: ICommand
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // private members                                                                                          //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Parent view model.</summary>
        internal MainViewModel _Parent;



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // constructors                                                                                             //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Creates a new instance of this class.</summary>
        /// <param name="parent">Parent view model.</param>
        internal GenerateReportCommand(MainViewModel parent)
        {
            _Parent = parent;

            _Parent.PropertyChanged += (sender, e) =>
            {
                if(e.PropertyName == nameof(_Parent.PlaceControlVisibility))
                {
                    CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                }
            };
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // [interface] ICommand                                                                                     //
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>Occures when the CanExecute() result has changed.</summary>
        public event EventHandler? CanExecuteChanged;


        /// <summary>Returns if the command can be executed.</summary>
        /// <param name="parameter">Parameter.</param>
        /// <returns>Returns TRUE when the command can be executed, otherwise returns FALSE.</returns>
        public bool CanExecute(object? parameter)
        {
            return (_Parent.PlaceControlVisibility == Visibility.Visible);
        }


        /// <summary>Executes the command.</summary>
        /// <param name="parameter">Paramter.</param>
        public void Execute(object? parameter)
        {
            string tmp = System.IO.Path.GetTempPath() + '\\' + Guid.NewGuid().ToString() + ".pdf";

            Reporting.GeneratePlaceReport(_Parent.PlaceDetails._Place ?? new(), tmp);
            Process p = new Process();
            p.StartInfo.FileName = tmp;
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }
    }
}
