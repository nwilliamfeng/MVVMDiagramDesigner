using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramDesigner;
using System.Windows.Input;

namespace DemoApp
{
    public class TankViewModel:DesignerElement
    {

        private ICommand _confirmCommand;

        public ICommand ConfirmCommand => this._confirmCommand ?? (_confirmCommand = new RelayCommand(() =>
              {
                  this.IsOpenSettingDialog = false;
                  this.Percent = this.TmpPercent;
              }));

        private ICommand _cancelCommand;

        public ICommand CancelCommand => this._cancelCommand ?? (_cancelCommand = new RelayCommand(() =>
        {
            this.IsOpenSettingDialog = false;
        }));

        private bool _isOpenSettingDialog;

        public bool IsOpenSettingDialog
        {
            get => _isOpenSettingDialog;
            set => this.Set(ref _isOpenSettingDialog, value);
 
        }

        private double _percent;

        public double Percent
        {
            get => _percent;
            set => this.Set(ref _percent, value);
        }

        private double _tmpPercent;

        public double TmpPercent
        {
            get => _tmpPercent;
            set => this.Set(ref _tmpPercent, value);
        }

        public TankViewModel()
        {
           
        }

        protected override void OnDoubleClick()
        {
            this.TmpPercent = this.Percent;
            this.IsOpenSettingDialog = true;
        }

    }
}
