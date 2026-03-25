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
    /// A designer class for a strongly typed repeater <c>BbAdditionalUserDetailRepeater</c>
    /// </summary>
	public class BbAdditionalUserDetailRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbAdditionalUserDetailRepeaterDesigner"/> class.
        /// </summary>
		public BbAdditionalUserDetailRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is BbAdditionalUserDetailRepeater))
			{ 
				throw new ArgumentException("Component is not a BbAdditionalUserDetailRepeater."); 
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
			BbAdditionalUserDetailRepeater z = (BbAdditionalUserDetailRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="BbAdditionalUserDetailRepeater"/> Type.
    /// </summary>
	[Designer(typeof(BbAdditionalUserDetailRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:BbAdditionalUserDetailRepeater runat=\"server\"></{0}:BbAdditionalUserDetailRepeater>")]
	public class BbAdditionalUserDetailRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbAdditionalUserDetailRepeater"/> class.
        /// </summary>
		public BbAdditionalUserDetailRepeater()
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
		[TemplateContainer(typeof(BbAdditionalUserDetailItem))]
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
		[TemplateContainer(typeof(BbAdditionalUserDetailItem))]
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
        [TemplateContainer(typeof(BbAdditionalUserDetailItem))]
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
		[TemplateContainer(typeof(BbAdditionalUserDetailItem))]
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
		[TemplateContainer(typeof(BbAdditionalUserDetailItem))]
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
						MHS.Badbir.NetTiers.Entities.BbAdditionalUserDetail entity = o as MHS.Badbir.NetTiers.Entities.BbAdditionalUserDetail;
						BbAdditionalUserDetailItem container = new BbAdditionalUserDetailItem(entity);
	
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
	public class BbAdditionalUserDetailItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MHS.Badbir.NetTiers.Entities.BbAdditionalUserDetail _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbAdditionalUserDetailItem"/> class.
        /// </summary>
		public BbAdditionalUserDetailItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbAdditionalUserDetailItem"/> class.
        /// </summary>
		public BbAdditionalUserDetailItem(MHS.Badbir.NetTiers.Entities.BbAdditionalUserDetail entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the Userid
        /// </summary>
        /// <value>The Userid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Guid Userid
		{
			get { return _entity.Userid; }
		}
        /// <summary>
        /// Gets the BadbiRuserid
        /// </summary>
        /// <value>The BadbiRuserid.</value>
		[System.ComponentModel.Bindable(true)]
		public int BadbiRuserid
		{
			get { return _entity.BadbiRuserid; }
		}
        /// <summary>
        /// Gets the Title
        /// </summary>
        /// <value>The Title.</value>
		[System.ComponentModel.Bindable(true)]
		public string Title
		{
			get { return _entity.Title; }
		}
        /// <summary>
        /// Gets the FName
        /// </summary>
        /// <value>The FName.</value>
		[System.ComponentModel.Bindable(true)]
		public string FName
		{
			get { return _entity.FName; }
		}
        /// <summary>
        /// Gets the LName
        /// </summary>
        /// <value>The LName.</value>
		[System.ComponentModel.Bindable(true)]
		public string LName
		{
			get { return _entity.LName; }
		}
        /// <summary>
        /// Gets the Position
        /// </summary>
        /// <value>The Position.</value>
		[System.ComponentModel.Bindable(true)]
		public string Position
		{
			get { return _entity.Position; }
		}
        /// <summary>
        /// Gets the Phone
        /// </summary>
        /// <value>The Phone.</value>
		[System.ComponentModel.Bindable(true)]
		public string Phone
		{
			get { return _entity.Phone; }
		}
        /// <summary>
        /// Gets the Hospital
        /// </summary>
        /// <value>The Hospital.</value>
		[System.ComponentModel.Bindable(true)]
		public string Hospital
		{
			get { return _entity.Hospital; }
		}
        /// <summary>
        /// Gets the Createdbyid
        /// </summary>
        /// <value>The Createdbyid.</value>
		[System.ComponentModel.Bindable(true)]
		public int Createdbyid
		{
			get { return _entity.Createdbyid; }
		}
        /// <summary>
        /// Gets the Createdbyname
        /// </summary>
        /// <value>The Createdbyname.</value>
		[System.ComponentModel.Bindable(true)]
		public string Createdbyname
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
		public int Lastupdatedbyid
		{
			get { return _entity.Lastupdatedbyid; }
		}
        /// <summary>
        /// Gets the Lastupdatedbyname
        /// </summary>
        /// <value>The Lastupdatedbyname.</value>
		[System.ComponentModel.Bindable(true)]
		public string Lastupdatedbyname
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
        /// Gets the BaselineTrainingDate
        /// </summary>
        /// <value>The BaselineTrainingDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? BaselineTrainingDate
		{
			get { return _entity.BaselineTrainingDate; }
		}
        /// <summary>
        /// Gets the BaselineTrainingBy
        /// </summary>
        /// <value>The BaselineTrainingBy.</value>
		[System.ComponentModel.Bindable(true)]
		public string BaselineTrainingBy
		{
			get { return _entity.BaselineTrainingBy; }
		}
        /// <summary>
        /// Gets the BaselineTrainingNotes
        /// </summary>
        /// <value>The BaselineTrainingNotes.</value>
		[System.ComponentModel.Bindable(true)]
		public string BaselineTrainingNotes
		{
			get { return _entity.BaselineTrainingNotes; }
		}
        /// <summary>
        /// Gets the FupTrainingDate
        /// </summary>
        /// <value>The FupTrainingDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? FupTrainingDate
		{
			get { return _entity.FupTrainingDate; }
		}
        /// <summary>
        /// Gets the FupTrainingBy
        /// </summary>
        /// <value>The FupTrainingBy.</value>
		[System.ComponentModel.Bindable(true)]
		public string FupTrainingBy
		{
			get { return _entity.FupTrainingBy; }
		}
        /// <summary>
        /// Gets the FupTrainingNotes
        /// </summary>
        /// <value>The FupTrainingNotes.</value>
		[System.ComponentModel.Bindable(true)]
		public string FupTrainingNotes
		{
			get { return _entity.FupTrainingNotes; }
		}
        /// <summary>
        /// Gets the Require2Fa
        /// </summary>
        /// <value>The Require2Fa.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Require2Fa
		{
			get { return _entity.Require2Fa; }
		}
        /// <summary>
        /// Gets the Flag1PersonalData
        /// </summary>
        /// <value>The Flag1PersonalData.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Flag1PersonalData
		{
			get { return _entity.Flag1PersonalData; }
		}
        /// <summary>
        /// Gets the Flag2EmailResearch
        /// </summary>
        /// <value>The Flag2EmailResearch.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Flag2EmailResearch
		{
			get { return _entity.Flag2EmailResearch; }
		}
        /// <summary>
        /// Gets the Flag3EmailNewsletter
        /// </summary>
        /// <value>The Flag3EmailNewsletter.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Flag3EmailNewsletter
		{
			get { return _entity.Flag3EmailNewsletter; }
		}
        /// <summary>
        /// Gets the Flag4EmailCentreRpt
        /// </summary>
        /// <value>The Flag4EmailCentreRpt.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Flag4EmailCentreRpt
		{
			get { return _entity.Flag4EmailCentreRpt; }
		}

        /// <summary>
        /// Gets a <see cref="T:MHS.Badbir.NetTiers.Entities.BbAdditionalUserDetail"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MHS.Badbir.NetTiers.Entities.BbAdditionalUserDetail Entity
        {
            get { return _entity; }
        }
	}
}
