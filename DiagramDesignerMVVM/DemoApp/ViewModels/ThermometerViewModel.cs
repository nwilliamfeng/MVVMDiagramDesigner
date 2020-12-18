using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiagramDesigner;
using System.Windows.Input;

namespace DemoApp
{
    public class ThermometerViewModel : DesignerElement
    {

        private ICommand _confirmCommand;

        public ICommand ConfirmCommand => this._confirmCommand ?? (_confirmCommand = new RelayCommand(() =>
        {
            this.IsOpenSettingDialog = false;
            this.Value = this.TmpValue;
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

        private double _value;

        public double Value
        {
            get => _value;
            set => this.Set(ref _value, value);
        }

        private double _tmpValue;

        public double TmpValue
        {
            get => _tmpValue;
            set => this.Set(ref _tmpValue, value);
        }

        public ThermometerViewModel()
        {

        }

        protected override void OnDoubleClick()
        {
            this.TmpValue = this.Value;
            this.IsOpenSettingDialog = true;
        }


    }
}
