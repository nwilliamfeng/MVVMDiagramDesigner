using System;
using System.Collections.Generic;
using System.Linq;
using DiagramDesigner;
using System.ComponentModel;
using System.Windows.Data;
 
using System.Threading.Tasks;
using System.Windows;

namespace DemoApp
{
    public class Window1ViewModel : NotifyObject
    {

        private List<int> _savedDiagrams = new List<int>();
        private List<VisualElement> _itemsToRemove;
        private IMessageBoxService _messageBoxService;
    
        private Diagram _diagramViewModel = new Diagram();

        public Window1ViewModel()
        {
            _messageBoxService = ApplicationServicesProvider.Instance.Provider.MessageBoxService;     
            ToolBoxViewModel = new ToolBoxViewModel();
            DiagramViewModel = new Diagram();
            
            DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
            // SaveDiagramCommand = new SimpleCommand(ExecuteSaveDiagramCommand);
            //LoadDiagramCommand = new SimpleCommand(ExecuteLoadDiagramCommand);
            //  GroupCommand = new SimpleCommand(ExecuteGroupCommand);

            SettingsDesignerItemViewModel item1 = new SettingsDesignerItemViewModel();
            item1.Parent = DiagramViewModel;
            item1.Left = 100;
            item1.Top = 100;
            DiagramViewModel.Items.Add(item1);

            PersistDesignerItemViewModel item2 = new PersistDesignerItemViewModel();
            item2.Parent = DiagramViewModel;
            item2.Left = 300;
            item2.Top = 300;
            DiagramViewModel.Items.Add(item2);

            item1.Connect(item2, ConnectorOrientation.Right, ConnectorOrientation.Left);
        }


        public SimpleCommand DeleteSelectedItemsCommand { get; private set; }
        public SimpleCommand CreateNewDiagramCommand { get; private set; }
        public SimpleCommand SaveDiagramCommand { get; private set; }
        public SimpleCommand GroupCommand { get; private set; }
        public SimpleCommand LoadDiagramCommand { get; private set; }
        public ToolBoxViewModel ToolBoxViewModel { get; private set; }


        public Diagram DiagramViewModel
        {
            get => _diagramViewModel;
            set
            {
                if (_diagramViewModel != value)
                {
                    _diagramViewModel = value;
                    NotifyOfPropertyChange("DiagramViewModel");
                }
            }
        }
 
        private void ExecuteDeleteSelectedItemsCommand(object parameter)=> DiagramViewModel.DeleteSelectedItems();


        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            //ensure that itemsToRemove is cleared ready for any new changes within a session
            _itemsToRemove = new List<VisualElement>();
           
            DiagramViewModel.CreateNewDiagramCommand.Execute(null);
        }
 

    }
}
