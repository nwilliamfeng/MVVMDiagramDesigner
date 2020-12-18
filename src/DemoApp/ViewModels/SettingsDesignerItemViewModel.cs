using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;
using System.ComponentModel;

namespace DemoApp
{
    [Description("设置器")]
    public class SettingsDesignerItemViewModel : DesignerElement, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;

        public SettingsDesignerItemViewModel(int id, IDiagram parent, double left, double top, string setting1)
            : base(id, parent, left, top)
        {

            this.Setting1 = setting1;
            Init();
        }

        public SettingsDesignerItemViewModel(int id, IDiagram parent, double left, double top, double itemWidth, double itemHeight, string setting1)
             : base(id, parent, left, top, itemWidth, itemHeight)
        {

            this.Setting1 = setting1;
            Init();
        }

        public SettingsDesignerItemViewModel()
        {
            Init();
        }

        public String Setting1 { get; set; }
        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand( )
        {
            SettingsDesignerItemData data = new SettingsDesignerItemData(Setting1);
            if (visualiserService.ShowDialog(data) == true)
            {
                this.Setting1 = data.Setting1;
            }
        }

        private void Init()
        {
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            ShowDataChangeWindowCommand = new RelayCommand(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;
        }
    }
}
