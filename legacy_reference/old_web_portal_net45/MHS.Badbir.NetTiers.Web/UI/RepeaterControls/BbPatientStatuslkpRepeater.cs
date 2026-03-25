using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using System.Web.UI.Design.WebControls;
using System.Web.UI.Design;
using System.Web.UI.WebControls;

namespace MHS.Badbir.NetTiers.Web.UI
{
    /// <summary>
    /// A designer class for a strongly typed repeater <c>BbPatientStatuslkpRepeater</c>
    /// </summary>
	public class BbPatientStatuslkpRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientStatuslkpRepeaterDesigner"/> class.
        /// </summary>
		public BbPatientStatuslkpRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is BbPatientStatuslkpRepeater))
			{ 
				throw new ArgumentException("Component is not a BbPatientStatuslkpRepeater."); 
			} 
			base.Initialize(component); 
			base.SetViewFlags(ViewFlags.TemplateEditing, true); 
		}


		/// <summary>
		/// Generate HTML for the designer
		/// </summary>
		/// <returns>a string of design time HTML</returns>
		public override string GetDesignTimeHtml()
		{

			// Get the instance this designer applies to
			//
			BbPatientStatuslkpRepeater z = (BbPatientStatuslkpRepeater)Component;
			z.DataBind();

			return base.GetDesignTimeHtml();

			//return z.RenderAtDesignTime();

			//	ControlCollection c = z.Controls;
			//Totem z = (Totem) Component;
			//Totem z = (Totem) Component;
			//return ("<div style='border: 1px gray dotted; background-color: lightgray'><b>TagStat :</b> zae |  qsdds</div>");

		}
	}

    /// <summary>
    /// A strongly typed repeater control for the <see cref="BbPatientStatuslkpRepeater"/> Type.
    /// </summary>
	[Designer(typeof(BbPatientStatuslkpRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:BbPatientStatuslkpRepeater runat=\"server\"></{0}:BbPatientStatuslkpRepeater>")]
	public class BbPatientStatuslkpRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientStatuslkpRepeater"/> class.
        /// </summary>
		public BbPatientStatuslkpRepeater()
		{
		}

		/// <summary>
        /// Gets a <see cref="T:System.Web.UI.ControlCollection"></see> object that represents the child controls for a specified server control in the UI hierarchy.
        /// </summary>
        /// <value></value>
        /// <returns>The collection of child controls for the specified server control.</returns>
		public override ControlCollection Controls
		{
			get
			{
				this.EnsureChildControls();
				return base.Controls;
			}
		}

		private ITemplate m_headerTemplate;
		/// <summary>
        /// Gets or sets the header template.
        /// </summary>
        /// <value>The header template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(BbPatientStatuslkpItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate HeaderTemplate
		{
			get { return m_headerTemplate; }
			set { m_headerTemplate = value; }
		}

		private ITemplate m_itemTemplate;
		/// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(BbPatientStatuslkpItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate ItemTemplate
		{
			get { return m_itemTemplate; }
			set { m_itemTemplate = value; }
		}

		private ITemplate m_seperatorTemplate;
        /// <summary>
        /// Gets or sets the Seperator Template
        /// </summary>
        [Browsable(false)]
        [TemplateContainer(typeof(BbPatientStatuslkpItem))]
        [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
        public ITemplate SeperatorTemplate
        {
            get { return m_seperatorTemplate; }
            set { m_seperatorTemplate = value; }
        }
			
		private ITemplate m_altenateItemTemplate;
        /// <summary>
        /// Gets or sets the alternating item template.
        /// </summary>
        /// <value>The alternating item template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(BbPatientStatuslkpItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate AlternatingItemTemplate
		{
			get { return m_altenateItemTemplate; }
			set { m_altenateItemTemplate = value; }
		}

		private ITemplate m_footerTemplate;
        /// <summary>
        /// Gets or sets the footer template.
        /// </summary>
        /// <value>The footer template.</value>
		[Browsable(false)]
		[TemplateContainer(typeof(BbPatientStatuslkpItem))]
		[PersistenceMode(PersistenceMode.InnerDefaultProperty)]
		public ITemplate FooterTemplate
		{
			get { return m_footerTemplate; }
			set { m_footerTemplate = value; }
		}

//      /// <summary>
//      /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
//      /// </summary>
//		protected override void CreateChildControls()
//      {
//         if (ChildControlsCreated)
//         {
//            return;
//         }

//         Controls.Clear();

//         //Instantiate the Header template (if exists)
//         if (m_headerTemplate != null)
//         {
//            Control headerItem = new Control();
//            m_headerTemplate.InstantiateIn(headerItem);
//            Controls.Add(headerItem);
//         }

//         //Instantiate the Footer template (if exists)
//         if (m_footerTemplate != null)
//         {
//            Control footerItem = new Control();
//            m_footerTemplate.InstantiateIn(footerItem);
//            Controls.Add(footerItem);
//         }
//
//         ChildControlsCreated = true;
//      }
	
		/// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderBeginTag(HtmlTextWriter writer)
        {
            
        }

        /// <summary>
        /// Overridden and Empty so that span tags are not written
        /// </summary>
        /// <param name="writer"></param>
        public override void RenderEndTag(HtmlTextWriter writer)
        {
                
        }		
		
		/// <summary>
      	/// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
      	/// </summary>
		protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
      	{
         int pos = 0;

         if (dataBinding)
         {
            //Instantiate the Header template (if exists)
            if (m_headerTemplate != null)
            {
                Control headerItem = new Control();
                m_headerTemplate.InstantiateIn(headerItem);
                Controls.Add(headerItem);
            }
			if (dataSource != null)
			{
				foreach (object o in dataSource)
				{
						MHS.Badbir.NetTiers.Entities.BbPatientStatuslkp entity = o as MHS.Badbir.NetTiers.Entities.BbPatientStatuslkp;
						BbPatientStatuslkpItem container = new BbPatientStatuslkpItem(entity);
	
						if (m_itemTemplate != null && (pos % 2) == 0)
						{
							m_itemTemplate.InstantiateIn(container);
							
							if (m_seperatorTemplate != null)
							{
								m_seperatorTemplate.InstantiateIn(container);
							}
						}
						else
						{
							if (m_altenateItemTemplate != null)
							{
								m_altenateItemTemplate.InstantiateIn(container);
								
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}
								
							}
							else if (m_itemTemplate != null)
							{
								m_itemTemplate.InstantiateIn(container);
								
								if (m_seperatorTemplate != null)
								{
									m_seperatorTemplate.InstantiateIn(container);
								}
							}
							else
							{
								// no template !!!
							}
						}
						Controls.Add(container);
						
						container.DataBind();
						
						pos++;
				}
			}
            //Instantiate the Footer template (if exists)
            if (m_footerTemplate != null)
            {
                Control footerItem = new Control();
                m_footerTemplate.InstantiateIn(footerItem);
                Controls.Add(footerItem);
            }

		}
			
			return pos;
		}

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.PreRender"></see> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
		protected override void OnPreRender(EventArgs e)
		{
			base.DataBind();
		}

		#region Design time
        /// <summary>
        /// Renders at design time.
        /// </summary>
        /// <returns>a  string of the Designed HTML</returns>
		internal string RenderAtDesignTime()
		{			
			return "Designer currently not implemented"; 
		}

		#endregion
	}

    /// <summary>
    /// A wrapper type for the entity
    /// </summary>
	[System.ComponentModel.ToolboxItem(false)]
	public class BbPatientStatuslkpItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MHS.Badbir.NetTiers.Entities.BbPatientStatuslkp _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientStatuslkpItem"/> class.
        /// </summary>
		public BbPatientStatuslkpItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientStatuslkpItem"/> class.
        /// </summary>
		public BbPatientStatuslkpItem(MHS.Badbir.NetTiers.Entities.BbPatientStatuslkp entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the Pstatusid
        /// </summary>
        /// <value>The Pstatusid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 Pstatusid
		{
			get { return _entity.Pstatusid; }
		}
        /// <summary>
        /// Gets the Pstatus
        /// </summary>
        /// <value>The Pstatus.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Pstatus
		{
			get { return _entity.Pstatus; }
		}
        /// <summary>
        /// Gets the Createdbyid
        /// </summary>
        /// <value>The Createdbyid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 Createdbyid
		{
			get { return _entity.Createdbyid; }
		}
        /// <summary>
        /// Gets the Createdbyname
        /// </summary>
        /// <value>The Createdbyname.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Createdbyname
		{
			get { return _entity.Createdbyname; }
		}
        /// <summary>
        /// Gets the Createddate
        /// </summary>
        /// <value>The Createddate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime Createddate
		{
			get { return _entity.Createddate; }
		}
        /// <summary>
        /// Gets the Lastupdatedbyid
        /// </summary>
        /// <value>The Lastupdatedbyid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 Lastupdatedbyid
		{
			get { return _entity.Lastupdatedbyid; }
		}
        /// <summary>
        /// Gets the Lastupdatedbyname
        /// </summary>
        /// <value>The Lastupdatedbyname.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Lastupdatedbyname
		{
			get { return _entity.Lastupdatedbyname; }
		}
        /// <summary>
        /// Gets the Lastupdateddate
        /// </summary>
        /// <value>The Lastupdateddate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime Lastupdateddate
		{
			get { return _entity.Lastupdateddate; }
		}
        /// <summary>
        /// Gets the IsValidStatus
        /// </summary>
        /// <value>The IsValidStatus.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? IsValidStatus
		{
			get { return _entity.IsValidStatus; }
		}
        /// <summary>
        /// Gets the IsNotLiveStatus
        /// </summary>
        /// <value>The IsNotLiveStatus.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? IsNotLiveStatus
		{
			get { return _entity.IsNotLiveStatus; }
		}
        /// <summary>
        /// Gets the IsNotTrainingStatus
        /// </summary>
        /// <value>The IsNotTrainingStatus.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? IsNotTrainingStatus
		{
			get { return _entity.IsNotTrainingStatus; }
		}
        /// <summary>
        /// Gets the ShowInClinicianList
        /// </summary>
        /// <value>The ShowInClinicianList.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? ShowInClinicianList
		{
			get { return _entity.ShowInClinicianList; }
		}

        /// <summary>
        /// Gets a <see cref="T:MHS.Badbir.NetTiers.Entities.BbPatientStatuslkp"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MHS.Badbir.NetTiers.Entities.BbPatientStatuslkp Entity
        {
            get { return _entity; }
        }
	}
}
