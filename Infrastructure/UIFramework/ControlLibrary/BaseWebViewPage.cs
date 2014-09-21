using BallyTech.UI.Web.ResourceManager;
using System.Web.Mvc;
using System.Web.Routing;

namespace BallyTech.UI.Web.ControlLibrary
{
    public abstract class BaseWebViewPage : WebViewPage
    {
        public ControlLib ControlLib { get; private set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            ControlLib = ControlLibFactory.Create(this);
        }
    }

    public abstract class BaseWebViewPage<TModel> : BaseWebViewPage
    {
        public new ControlLib<TModel> ControlLib { get; private set; }

        public override void InitHelpers()
        {
            base.InitHelpers();
            ControlLib = ControlLibFactory.Create(this);
        }
    }

    public class ControlLib : HtmlHelper
    {
        private readonly IConfigurationReader _configReader;

        private readonly IResourceService _resourceService;

        public ControlLib(ViewContext viewContext,
          IViewDataContainer viewDataContainer)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
        }

        public ControlLib(ViewContext viewContext, IViewDataContainer viewDataContainer, IConfigurationReader configReader, IResourceService resourceService)
            : this(viewContext, viewDataContainer, RouteTable.Routes)
        {
            this._configReader = configReader;
            this._resourceService = resourceService;
        }

        public ControlLib(ViewContext viewContext,
           IViewDataContainer viewDataContainer, RouteCollection routeCollection)
            : base(viewContext, viewDataContainer, routeCollection)
        {
            ViewContext = viewContext;
            ViewData = new ViewDataDictionary(viewDataContainer.ViewData);
        }

        public ViewDataDictionary ViewData
        {
            get;
            private set;
        }

        public ViewContext ViewContext
        {
            get;
            private set;
        }

        public IConfigurationReader ConfigReader
        {
            get
            {
                return _configReader;
            }
        }

        public IResourceService ResourceService
        {
            get
            {
                return _resourceService;
            }
        }
    }

    public class ControlLib<TModel> : HtmlHelper<TModel>
    {

        private readonly IConfigurationReader _configReader;

        private readonly IResourceService _resourceService;

        public ControlLib(ViewContext viewContext, IViewDataContainer container)
            : this(viewContext, container, RouteTable.Routes)
        {
        }

        public ControlLib(ViewContext viewContext, IViewDataContainer container, IConfigurationReader configReader, IResourceService resourceService)
            : this(viewContext, container, RouteTable.Routes)
        {
            _configReader = configReader;
            _resourceService = resourceService;
        }

        public ControlLib(ViewContext viewContext, IViewDataContainer container,
            RouteCollection routeCollection)
            : base(viewContext, container,
                routeCollection)
        {
            ViewData = new ViewDataDictionary<TModel>(container.ViewData);
            ViewContext = viewContext;
        }

        public ViewDataDictionary<TModel> ViewData
        {
            get;
            private set;
        }

        public ViewContext ViewContext
        {
            get;
            private set;
        }

        public IConfigurationReader ConfigReader
        {
            get
            {
                return _configReader;
            }
        }

        public IResourceService ResourceService
        {
            get
            {
                return _resourceService;
            }
        }
    }

    public static class ControlLibFactory
    {
        public static ControlLib Create(BaseWebViewPage baseWebViewPage)
        {
            var controlLib = new ControlLib(baseWebViewPage.ViewContext, baseWebViewPage, ControlLibraryConfig.ControlConfigReader, ControlLibraryConfig.ResourceService);
            return controlLib;
        }

        public static ControlLib<TModel> Create<TModel>(BaseWebViewPage<TModel> baseWebViewPage)
        {
            var controlLib = new ControlLib<TModel>(baseWebViewPage.ViewContext, baseWebViewPage, ControlLibraryConfig.ControlConfigReader, ControlLibraryConfig.ResourceService);
            return controlLib;
        }
    }
}