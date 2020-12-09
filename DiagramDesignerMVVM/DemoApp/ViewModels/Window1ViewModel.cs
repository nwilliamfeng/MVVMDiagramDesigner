using System;
using System.Collections.Generic;
using System.Linq;
using DiagramDesigner;
using System.ComponentModel;
using System.Windows.Data;
using DemoApp.Persistence.Common;
using System.Threading.Tasks;
using System.Windows;

namespace DemoApp
{
    public class Window1ViewModel : NotifyObject
    {

        private List<int> _savedDiagrams = new List<int>();
        private List<SelectableDesignerItem> _itemsToRemove;
        private IMessageBoxService _messageBoxService;
        private IDatabaseAccessService _databaseAccessService;
        private Diagram _diagramViewModel = new Diagram();

        public Window1ViewModel()
        {
            _messageBoxService = ApplicationServicesProvider.Instance.Provider.MessageBoxService;
            _databaseAccessService = ApplicationServicesProvider.Instance.Provider.DatabaseAccessService;

            foreach (var savedDiagram in _databaseAccessService.FetchAllDiagram())
            {
                _savedDiagrams.Add(savedDiagram.Id);
            }

            ToolBoxViewModel = new ToolBoxViewModel();
            DiagramViewModel = new Diagram();
            
            DeleteSelectedItemsCommand = new SimpleCommand(ExecuteDeleteSelectedItemsCommand);
            CreateNewDiagramCommand = new SimpleCommand(ExecuteCreateNewDiagramCommand);
           // SaveDiagramCommand = new SimpleCommand(ExecuteSaveDiagramCommand);
            //LoadDiagramCommand = new SimpleCommand(ExecuteLoadDiagramCommand);
          //  GroupCommand = new SimpleCommand(ExecuteGroupCommand);

          
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
 
        private void ExecuteDeleteSelectedItemsCommand(object parameter)
        {
            _itemsToRemove = DiagramViewModel.SelectedItems;
            List<SelectableDesignerItem> connectionsToAlsoRemove = new List<SelectableDesignerItem>();

            foreach (var connector in DiagramViewModel.Items.OfType<ConnectorDesignerItem>())
            {
                if (ItemsToDeleteHasConnector(_itemsToRemove, connector.SourceConnectorInfo))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

                if (ItemsToDeleteHasConnector(_itemsToRemove, (FullyCreatedConnectorInfo)connector.SinkConnectorInfo))
                {
                    connectionsToAlsoRemove.Add(connector);
                }

            }
            _itemsToRemove.AddRange(connectionsToAlsoRemove);
            foreach (var selectedItem in _itemsToRemove)
            {
                DiagramViewModel.RemoveItemCommand.Execute(selectedItem);
            }
        }

        private void ExecuteCreateNewDiagramCommand(object parameter)
        {
            //ensure that itemsToRemove is cleared ready for any new changes within a session
            _itemsToRemove = new List<SelectableDesignerItem>();
           
            DiagramViewModel.CreateNewDiagramCommand.Execute(null);
        }

    
        private bool ItemsToDeleteHasConnector(List<SelectableDesignerItem> itemsToRemove, FullyCreatedConnectorInfo connector)
        {
            return itemsToRemove.Contains(connector.DataItem);
        }


 

    }
}
