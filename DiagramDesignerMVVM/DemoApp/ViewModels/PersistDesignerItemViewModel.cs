﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiagramDesigner;
using System.Windows.Input;
using System.Windows;

namespace DemoApp
{
    public class PersistDesignerItemViewModel : DesignerElement, ISupportDataChanges
    {
        private IUIVisualizerService visualiserService;

        public PersistDesignerItemViewModel(int id, IDiagram parent, double left, double top, string hostUrl) : base(id,parent, left,top)
        {
            this.HostUrl = hostUrl;
            Init();
        }
        public PersistDesignerItemViewModel(int id, IDiagram parent, double left, double top, double itemWidth, double itemHeight, string hostUrl) : base(id, parent, left, top, itemWidth, itemHeight)
        {
            this.HostUrl = hostUrl;
            Init();
        }

        public PersistDesignerItemViewModel() : base()
        {
            Init();
        }


        public String HostUrl { get; set; }
        public ICommand ShowDataChangeWindowCommand { get; private set; }

        public void ExecuteShowDataChangeWindowCommand( )
        {
            PersistDesignerItemData data = new PersistDesignerItemData(HostUrl);
            if (visualiserService.ShowDialog(data) == true)
            {
                this.HostUrl = data.HostUrl;
            }
        }

        protected override void OnDoubleClick()
        {
            ExecuteShowDataChangeWindowCommand();
        }


        private void Init()
        {
            visualiserService = ApplicationServicesProvider.Instance.Provider.VisualizerService;
            ShowDataChangeWindowCommand = new RelayCommand(ExecuteShowDataChangeWindowCommand);
            this.ShowConnectors = false;
            DesignerMenuItem menuItem1 = new DesignerMenuItem { Name = "aaa" };
            menuItem1.Items.Add(new DesignerMenuItem { Name = "a2", Command = new RelayCommand(() =>
                 {
                     MessageBox.Show("a2 click");
                 }) });
            var menuItem2 = new DesignerMenuItem
            {
                Name = "bbb",
                Command = new RelayCommand(() => MessageBox.Show("bbb click") )
            };
            this.MenuItems.Add(menuItem1);
            this.MenuItems.Add(menuItem2);
        }
    }
}
