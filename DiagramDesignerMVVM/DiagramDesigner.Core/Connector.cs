using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using DiagramDesigner.Helpers;

namespace DiagramDesigner
{
    /// <summary>
    /// 连接器设计项
    /// </summary>
    public class Connector : VisualElement 
    {
        private FullyCreatedConnectorInfo _sourceConnectorInfo;
        private ConnectorInfoBase _sinkConnectorInfo;
        private Point _sourceB;
        private Point _sourceA;
        private List<Point> _connectionPoints;
        private Point _endPoint;
        private Rect _area;
        private double _lineThickness=3;
        private ConnectorLineType _lineType ;
        private bool _showArrow = false;


        public Connector(int id, IDiagram parent, FullyCreatedConnectorInfo sourceConnectorInfo
            , FullyCreatedConnectorInfo sinkConnectorInfo) : base(id,parent)
        {
            Init(sourceConnectorInfo, sinkConnectorInfo);
        }

        public Connector(FullyCreatedConnectorInfo sourceConnectorInfo, ConnectorInfoBase sinkConnectorInfo)
        {
            Init(sourceConnectorInfo, sinkConnectorInfo);
        }

        /// <summary>
        /// todo-- 默认用OrthogonalPathFinder ，需要替换
        /// </summary>
        internal static IPathFinder PathFinder { get; set; }= new OrthogonalPathFinder();

        public bool IsFullConnection => _sinkConnectorInfo is FullyCreatedConnectorInfo;

        /// <summary>
        /// 线厚度
        /// </summary>
        public double LineThickness
        {
            get => _lineThickness;
            set => this.Set(ref _lineThickness, value);
        }

        /// <summary>
        /// 是否显示箭头
        /// </summary>
        public bool ShowArrow
        {
            get => _showArrow;
            set => this.Set(ref _showArrow, value);
        }

        /// <summary>
        /// 线类型
        /// </summary>
        public ConnectorLineType LineType
        {
            get => _lineType;
            set => this.Set(ref _lineType, value);
        }


        internal Point SourceA
        {
            get => _sourceA;
            set
            {
                if (_sourceA != value)
                {
                    _sourceA = value;
                    UpdateArea();
                    NotifyOfPropertyChange(()=>SourceA);
                }
            }
        }

        internal Point SourceB
        {
            get => _sourceB;
            set
            {
                if (_sourceB != value)
                {
                    _sourceB = value;
                    UpdateArea();
                    NotifyOfPropertyChange(()=>SourceB);
                }
            }
        }

        public List<Point> ConnectionPoints
        {
            get => _connectionPoints;
            private set => this.Set(ref _connectionPoints, value);          
        }

        public Point EndPoint
        {
            get => _endPoint;
            private set => this.Set(ref _endPoint, value);          
        }

        public Rect Area
        {
            get=> _area;
            private set
            {
                if (_area != value)
                {
                    _area = value;
                    UpdateConnectionPoints();
                    NotifyOfPropertyChange(()=>Area);
                }
            }
        }

        internal ConnectorInfoMeta ConnectorInfo(ConnectorOrientation orientation, double left, double top, Point position)=>
            new ConnectorInfoMeta()
            {
                Orientation = orientation,
                DesignerItemSize = new Size(_sourceConnectorInfo.DataItem.ItemWidth, _sourceConnectorInfo.DataItem.ItemHeight),
                DesignerItemLeft = left,
                DesignerItemTop = top,
                Position = position
            };


        public FullyCreatedConnectorInfo SourceConnectorInfo
        {
            get => _sourceConnectorInfo;
            set
            {
                if (_sourceConnectorInfo != value)
                {
                    _sourceConnectorInfo = value;
                    SourceA = PointHelper.GetPointForConnector(this.SourceConnectorInfo);
                    NotifyOfPropertyChange(()=>SourceConnectorInfo);
                    (_sourceConnectorInfo.DataItem as INotifyPropertyChanged).PropertyChanged += new WeakPropertyEventHandler(ConnectorViewModel_PropertyChanged).Handler;
                }
            }
        }

        public ConnectorInfoBase SinkConnectorInfo
        {
            get => _sinkConnectorInfo;
            set
            {
                if (_sinkConnectorInfo != value)
                {
                    _sinkConnectorInfo = value;
                    if (SinkConnectorInfo is FullyCreatedConnectorInfo)
                    {
                        SourceB = PointHelper.GetPointForConnector((FullyCreatedConnectorInfo)SinkConnectorInfo);
                        (((FullyCreatedConnectorInfo)_sinkConnectorInfo).DataItem as INotifyPropertyChanged).PropertyChanged += new WeakPropertyEventHandler(ConnectorViewModel_PropertyChanged).Handler;
                    }
                    else
                    {
                        SourceB = ((PartCreatedConnectionInfo)SinkConnectorInfo).CurrentLocation;
                    }
                    NotifyOfPropertyChange(()=>SinkConnectorInfo);
                }
            }
        }

        private void UpdateArea()
        {
            Area = new Rect(SourceA, SourceB); 
        }

        private void UpdateConnectionPoints()
        {
            ConnectionPoints = new List<Point>()
                                   {                                       
                                       new Point( SourceA.X  <  SourceB.X ? 0d : Area.Width, SourceA.Y  <  SourceB.Y ? 0d : Area.Height ), 
                                       new Point(SourceA.X  >  SourceB.X ? 0d : Area.Width, SourceA.Y  >  SourceB.Y ? 0d : Area.Height)
                                   };

            ConnectorInfoMeta sourceInfo = ConnectorInfo(SourceConnectorInfo.Orientation,
                                            ConnectionPoints[0].X,
                                            ConnectionPoints[0].Y,
                                            ConnectionPoints[0]);

            if(IsFullConnection)
            {
                EndPoint = ConnectionPoints.Last();
                ConnectorInfoMeta sinkInfo = ConnectorInfo(SinkConnectorInfo.Orientation,
                                  ConnectionPoints[1].X,
                                  ConnectionPoints[1].Y,
                                  ConnectionPoints[1]);

                ConnectionPoints = PathFinder.GetConnectionLine(sourceInfo, sinkInfo, true);
            }
            else
            {
                ConnectionPoints = PathFinder.GetConnectionLine(sourceInfo, ConnectionPoints[1], ConnectorOrientation.Left);
                EndPoint = new Point();
            }
        }

        private void ConnectorViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DesignerElement.ItemHeight):
                case nameof(DesignerElement.ItemWidth):
                case nameof(DesignerElement.Left):
                case nameof(DesignerElement.Top):
                    SourceA = PointHelper.GetPointForConnector(this.SourceConnectorInfo);
                    if (this.SinkConnectorInfo is FullyCreatedConnectorInfo)
                        SourceB = PointHelper.GetPointForConnector((FullyCreatedConnectorInfo)this.SinkConnectorInfo);
                    break;
            }
        }

        private void Init(FullyCreatedConnectorInfo sourceConnectorInfo, ConnectorInfoBase sinkConnectorInfo)
        {
            if (sourceConnectorInfo == null)
                return;
            this.Parent = sourceConnectorInfo.DataItem.Parent;
            this.SourceConnectorInfo = sourceConnectorInfo;
            this.SinkConnectorInfo = sinkConnectorInfo;
            PathFinder = new OrthogonalPathFinder();
        }

    }
}
