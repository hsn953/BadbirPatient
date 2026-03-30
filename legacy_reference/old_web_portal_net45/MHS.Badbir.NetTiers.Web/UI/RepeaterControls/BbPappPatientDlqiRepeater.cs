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
    /// A designer class for a strongly typed repeater <c>BbPappPatientDlqiRepeater</c>
    /// </summary>
	public class BbPappPatientDlqiRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPappPatientDlqiRepeaterDesigner"/> class.
        /// </summary>
		public BbPappPatientDlqiRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is BbPappPatientDlqiRepeater))
			{ 
				throw new ArgumentException("Component is not a BbPappPatientDlqiRepeater."); 
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
			BbPappPatientDlqiRepeater z = (BbPappPatientDlqiRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="BbPappPatientDlqiRepeater"/> Type.
    /// </summary>
	[Designer(typeof(BbPappPatientDlqiRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:BbPappPatientDlqiRepeater runat=\"server\"></{0}:BbPappPatientDlqiRepeater>")]
	public class BbPappPatientDlqiRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPappPatientDlqiRepeater"/> class.
        /// </summary>
		public BbPappPatientDlqiRepeater()
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
		[TemplateContainer(typeof(BbPappPatientDlqiItem))]
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
		[TemplateContainer(typeof(BbPappPatientDlqiItem))]
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
        [TemplateContainer(typeof(BbPappPatientDlqiItem))]
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
		[TemplateContainer(typeof(BbPappPatientDlqiItem))]
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
		[TemplateContainer(typeof(BbPappPatientDlqiItem))]
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
						MHS.Badbir.NetTiers.Entities.BbPappPatientDlqi entity = o as MHS.Badbir.NetTiers.Entities.BbPappPatientDlqi;
						BbPappPatientDlqiItem container = new BbPappPatientDlqiItem(entity);
	
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
	public class BbPappPatientDlqiItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MHS.Badbir.NetTiers.Entities.BbPappPatientDlqi _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPappPatientDlqiItem"/> class.
        /// </summary>
		public BbPappPatientDlqiItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPappPatientDlqiItem"/> class.
        /// </summary>
		public BbPappPatientDlqiItem(MHS.Badbir.NetTiers.Entities.BbPappPatientDlqi entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the FormId
        /// </summary>
        /// <value>The FormId.</value>
		[System.ComponentModel.Bindable(true)]
		public int FormId
		{
			get { return _entity.FormId; }
		}
        /// <summary>
        /// Gets the Chid
        /// </summary>
        /// <value>The Chid.</value>
		[System.ComponentModel.Bindable(true)]
		public int Chid
		{
			get { return _entity.Chid; }
		}
        /// <summary>
        /// Gets the PappFupId
        /// </summary>
        /// <value>The PappFupId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? PappFupId
		{
			get { return _entity.PappFupId; }
		}
        /// <summary>
        /// Gets the Diagnosis
        /// </summary>
        /// <value>The Diagnosis.</value>
		[System.ComponentModel.Bindable(true)]
		public string Diagnosis
		{
			get { return _entity.Diagnosis; }
		}
        /// <summary>
        /// Gets the ItchsoreScore
        /// </summary>
        /// <value>The ItchsoreScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? ItchsoreScore
		{
			get { return _entity.ItchsoreScore; }
		}
        /// <summary>
        /// Gets the EmbscScore
        /// </summary>
        /// <value>The EmbscScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? EmbscScore
		{
			get { return _entity.EmbscScore; }
		}
        /// <summary>
        /// Gets the ShophgScore
        /// </summary>
        /// <value>The ShophgScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? ShophgScore
		{
			get { return _entity.ShophgScore; }
		}
        /// <summary>
        /// Gets the ClothesScore
        /// </summary>
        /// <value>The ClothesScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? ClothesScore
		{
			get { return _entity.ClothesScore; }
		}
        /// <summary>
        /// Gets the SocleisScore
        /// </summary>
        /// <value>The SocleisScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? SocleisScore
		{
			get { return _entity.SocleisScore; }
		}
        /// <summary>
        /// Gets the SportScore
        /// </summary>
        /// <value>The SportScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? SportScore
		{
			get { return _entity.SportScore; }
		}
        /// <summary>
        /// Gets the WorkstudScore
        /// </summary>
        /// <value>The WorkstudScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? WorkstudScore
		{
			get { return _entity.WorkstudScore; }
		}
        /// <summary>
        /// Gets the WorkstudnoScore
        /// </summary>
        /// <value>The WorkstudnoScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? WorkstudnoScore
		{
			get { return _entity.WorkstudnoScore; }
		}
        /// <summary>
        /// Gets the PartcrfScore
        /// </summary>
        /// <value>The PartcrfScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? PartcrfScore
		{
			get { return _entity.PartcrfScore; }
		}
        /// <summary>
        /// Gets the SexdifScore
        /// </summary>
        /// <value>The SexdifScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? SexdifScore
		{
			get { return _entity.SexdifScore; }
		}
        /// <summary>
        /// Gets the TreatmentScore
        /// </summary>
        /// <value>The TreatmentScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? TreatmentScore
		{
			get { return _entity.TreatmentScore; }
		}
        /// <summary>
        /// Gets the TotalScore
        /// </summary>
        /// <value>The TotalScore.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? TotalScore
		{
			get { return _entity.TotalScore; }
		}
        /// <summary>
        /// Gets the Datecomp
        /// </summary>
        /// <value>The Datecomp.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Datecomp
		{
			get { return _entity.Datecomp; }
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
        /// Gets the SkipBreakup
        /// </summary>
        /// <value>The SkipBreakup.</value>
		[System.ComponentModel.Bindable(true)]
		public int SkipBreakup
		{
			get { return _entity.SkipBreakup; }
		}
        /// <summary>
        /// Gets the DlqiMissing
        /// </summary>
        /// <value>The DlqiMissing.</value>
		[System.ComponentModel.Bindable(true)]
		public bool DlqiMissing
		{
			get { return _entity.DlqiMissing; }
		}

        /// <summary>
        /// Gets a <see cref="T:MHS.Badbir.NetTiers.Entities.BbPappPatientDlqi"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MHS.Badbir.NetTiers.Entities.BbPappPatientDlqi Entity
        {
            get { return _entity; }
        }
	}
}
