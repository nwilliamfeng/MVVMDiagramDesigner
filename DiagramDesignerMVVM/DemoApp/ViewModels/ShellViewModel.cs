using System;
using System.Collections.Generic;
using System.Linq;
using DiagramDesigner;
using System.ComponentModel;
using System.Windows.Data;
using Caliburn.Micro;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel.Composition;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DemoApp
{
    [Export(typeof(ShellViewModel))]
    public class ShellViewModel : Screen
    {

        private List<int> _savedDiagrams = new List<int>();
        private List<VisualElement> _itemsToRemove;
        private IMessageBoxService _messageBoxService;
    
        private Diagram _diagramViewModel = new Diagram();

        public ShellViewModel()
        {
            _messageBoxService = ApplicationServicesProvider.Instance.Provider.MessageBoxService;     
       
            Diagram = new Diagram();
            
            DeleteSelectedItemsCommand = new RelayCommand(ExecuteDeleteSelectedItemsCommand);
            CreateNewDiagramCommand = new RelayCommand(ExecuteCreateNewDiagramCommand);
            // SaveDiagramCommand = new SimpleCommand(ExecuteSaveDiagramCommand);
            //LoadDiagramCommand = new SimpleCommand(ExecuteLoadDiagramCommand);
            //  GroupCommand = new SimpleCommand(ExecuteGroupCommand);

            SettingsDesignerItemViewModel item1 = new SettingsDesignerItemViewModel();
            item1.Parent = Diagram;
            item1.Left = 100;
            item1.Top = 100;
            Diagram.Items.Add(item1);

            PersistDesignerItemViewModel item2 = new PersistDesignerItemViewModel();
            item2.Parent = Diagram;
            item2.Left = 300;
            item2.Top = 300;
            Diagram.Items.Add(item2);

            item1.Connect(item2, ConnectorOrientation.Right, ConnectorOrientation.Left);

            this.Tools.Add(new SettingsDesignerItemViewModel());
            this.Tools.Add(new PersistDesignerItemViewModel());
            this.Tools.Add(new FanViewModel());
            this.Tools.Add(new LightViewModel());
            this.Tools.Add(new TankViewModel {  Percent=20});
            this.Tools.Add(new ThermometerViewModel { Value = 50 });
        }


        public ICommand DeleteSelectedItemsCommand { get; private set; }
        public ICommand CreateNewDiagramCommand { get; private set; }
        public ICommand SaveDiagramCommand { get; private set; }
        public ICommand GroupCommand { get; private set; }
        public ICommand LoadDiagramCommand { get; private set; }
       

        public Diagram Diagram
        {
            get => _diagramViewModel;
            set => this.Set(ref _diagramViewModel, value);
          
        }
 
        private void ExecuteDeleteSelectedItemsCommand()=> Diagram.DeleteSelectedItems();


        private void ExecuteCreateNewDiagramCommand( )
        {
            _itemsToRemove = new List<VisualElement>();          
            Diagram.CreateNewDiagramCommand.Execute(null);
        }

       

        public ConnectorLineType? LineType
        {
            get
            {
                if (this.Diagram.SelectedItems.Count == 0) return null;
                if (this.Diagram.SelectedItems.Any(x => !(x is Connector))) return null;
                var connectors = this.Diagram.SelectedItems.OfType<Connector>().ToList();
                if (connectors.GroupBy(x => x.LineType).Count() > 1) return null;
                return connectors.First().LineType;
            }
            set
            {
                if (value != null)
                    this.Diagram.SelectedItems.OfType<Connector>().ToList().ForEach(x =>
                    {
                        x.LineType = value.Value;
                    });
            }
        }

        private bool _showLineArrow;
        public bool ShowLineArrow
        {
            get
            {           
                return _showLineArrow;
            }
            set
            {
       
                this.Diagram.SelectedItems.OfType<Connector>().ToList()
                    .ForEach(x=>
                    {
                        x.ShowArrow = value;
                    });
                
                this.Set(ref _showLineArrow, value);
            }
        }


        private bool _showGridLines;

        public bool ShowGridLines
        {
            get => _showGridLines;
            set => this.Set(ref _showGridLines, value);
        }

        public ObservableCollection<DesignerElement> Tools { get; private set; } = new ObservableCollection<DesignerElement>();
 
    }
}
