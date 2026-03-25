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
    /// A designer class for a strongly typed repeater <c>BbPatientCohortTrackingRepeater</c>
    /// </summary>
	public class BbPatientCohortTrackingRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientCohortTrackingRepeaterDesigner"/> class.
        /// </summary>
		public BbPatientCohortTrackingRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is BbPatientCohortTrackingRepeater))
			{ 
				throw new ArgumentException("Component is not a BbPatientCohortTrackingRepeater."); 
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
			BbPatientCohortTrackingRepeater z = (BbPatientCohortTrackingRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="BbPatientCohortTrackingRepeater"/> Type.
    /// </summary>
	[Designer(typeof(BbPatientCohortTrackingRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:BbPatientCohortTrackingRepeater runat=\"server\"></{0}:BbPatientCohortTrackingRepeater>")]
	public class BbPatientCohortTrackingRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientCohortTrackingRepeater"/> class.
        /// </summary>
		public BbPatientCohortTrackingRepeater()
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
		[TemplateContainer(typeof(BbPatientCohortTrackingItem))]
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
		[TemplateContainer(typeof(BbPatientCohortTrackingItem))]
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
        [TemplateContainer(typeof(BbPatientCohortTrackingItem))]
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
		[TemplateContainer(typeof(BbPatientCohortTrackingItem))]
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
		[TemplateContainer(typeof(BbPatientCohortTrackingItem))]
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
						MHS.Badbir.NetTiers.Entities.BbPatientCohortTracking entity = o as MHS.Badbir.NetTiers.Entities.BbPatientCohortTracking;
						BbPatientCohortTrackingItem container = new BbPatientCohortTrackingItem(entity);
	
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
	public class BbPatientCohortTrackingItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MHS.Badbir.NetTiers.Entities.BbPatientCohortTracking _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientCohortTrackingItem"/> class.
        /// </summary>
		public BbPatientCohortTrackingItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientCohortTrackingItem"/> class.
        /// </summary>
		public BbPatientCohortTrackingItem(MHS.Badbir.NetTiers.Entities.BbPatientCohortTracking entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the FupId
        /// </summary>
        /// <value>The FupId.</value>
		[System.ComponentModel.Bindable(true)]
		public int FupId
		{
			get { return _entity.FupId; }
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
        /// Gets the Fupcode
        /// </summary>
        /// <value>The Fupcode.</value>
		[System.ComponentModel.Bindable(true)]
		public int Fupcode
		{
			get { return _entity.Fupcode; }
		}
        /// <summary>
        /// Gets the Studynocurrent
        /// </summary>
        /// <value>The Studynocurrent.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Studynocurrent
		{
			get { return _entity.Studynocurrent; }
		}
        /// <summary>
        /// Gets the Centreidcurrent
        /// </summary>
        /// <value>The Centreidcurrent.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Centreidcurrent
		{
			get { return _entity.Centreidcurrent; }
		}
        /// <summary>
        /// Gets the Consultantidcurrent
        /// </summary>
        /// <value>The Consultantidcurrent.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Consultantidcurrent
		{
			get { return _entity.Consultantidcurrent; }
		}
        /// <summary>
        /// Gets the ClinicVisitdate
        /// </summary>
        /// <value>The ClinicVisitdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? ClinicVisitdate
		{
			get { return _entity.ClinicVisitdate; }
		}
        /// <summary>
        /// Gets the Duedate
        /// </summary>
        /// <value>The Duedate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Duedate
		{
			get { return _entity.Duedate; }
		}
        /// <summary>
        /// Gets the EditWindowFrom
        /// </summary>
        /// <value>The EditWindowFrom.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? EditWindowFrom
		{
			get { return _entity.EditWindowFrom; }
		}
        /// <summary>
        /// Gets the EditWindowTo
        /// </summary>
        /// <value>The EditWindowTo.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? EditWindowTo
		{
			get { return _entity.EditWindowTo; }
		}
        /// <summary>
        /// Gets the Fupstatus
        /// </summary>
        /// <value>The Fupstatus.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Fupstatus
		{
			get { return _entity.Fupstatus; }
		}
        /// <summary>
        /// Gets the Datavalid
        /// </summary>
        /// <value>The Datavalid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Datavalid
		{
			get { return _entity.Datavalid; }
		}
        /// <summary>
        /// Gets the Feedbackstatus
        /// </summary>
        /// <value>The Feedbackstatus.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Feedbackstatus
		{
			get { return _entity.Feedbackstatus; }
		}
        /// <summary>
        /// Gets the Comments
        /// </summary>
        /// <value>The Comments.</value>
		[System.ComponentModel.Bindable(true)]
		public string Comments
		{
			get { return _entity.Comments; }
		}
        /// <summary>
        /// Gets the Dateentered
        /// </summary>
        /// <value>The Dateentered.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Dateentered
		{
			get { return _entity.Dateentered; }
		}
        /// <summary>
        /// Gets the Hasnocurrenttherapy
        /// </summary>
        /// <value>The Hasnocurrenttherapy.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Hasnocurrenttherapy
		{
			get { return _entity.Hasnocurrenttherapy; }
		}
        /// <summary>
        /// Gets the Hasnobiologictherapy
        /// </summary>
        /// <value>The Hasnobiologictherapy.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnobiologictherapy
		{
			get { return _entity.Hasnobiologictherapy; }
		}
        /// <summary>
        /// Gets the Hasnoconventionaltherapy
        /// </summary>
        /// <value>The Hasnoconventionaltherapy.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnoconventionaltherapy
		{
			get { return _entity.Hasnoconventionaltherapy; }
		}
        /// <summary>
        /// Gets the Hasnoprevioustherapy
        /// </summary>
        /// <value>The Hasnoprevioustherapy.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnoprevioustherapy
		{
			get { return _entity.Hasnoprevioustherapy; }
		}
        /// <summary>
        /// Gets the Hasnocomorbidities
        /// </summary>
        /// <value>The Hasnocomorbidities.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Hasnocomorbidities
		{
			get { return _entity.Hasnocomorbidities; }
		}
        /// <summary>
        /// Gets the Hasnolesions
        /// </summary>
        /// <value>The Hasnolesions.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Hasnolesions
		{
			get { return _entity.Hasnolesions; }
		}
        /// <summary>
        /// Gets the Hasnouvtherapy
        /// </summary>
        /// <value>The Hasnouvtherapy.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Hasnouvtherapy
		{
			get { return _entity.Hasnouvtherapy; }
		}
        /// <summary>
        /// Gets the Hasnolabvalues
        /// </summary>
        /// <value>The Hasnolabvalues.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnolabvalues
		{
			get { return _entity.Hasnolabvalues; }
		}
        /// <summary>
        /// Gets the Hasnoadverseevents
        /// </summary>
        /// <value>The Hasnoadverseevents.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnoadverseevents
		{
			get { return _entity.Hasnoadverseevents; }
		}
        /// <summary>
        /// Gets the Hasnodiseaseseverity
        /// </summary>
        /// <value>The Hasnodiseaseseverity.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnodiseaseseverity
		{
			get { return _entity.Hasnodiseaseseverity; }
		}
        /// <summary>
        /// Gets the Hasnopasi
        /// </summary>
        /// <value>The Hasnopasi.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Hasnopasi
		{
			get { return _entity.Hasnopasi; }
		}
        /// <summary>
        /// Gets the Hasnoadditionalinfo
        /// </summary>
        /// <value>The Hasnoadditionalinfo.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Hasnoadditionalinfo
		{
			get { return _entity.Hasnoadditionalinfo; }
		}
        /// <summary>
        /// Gets the Hasnomedicalproblems
        /// </summary>
        /// <value>The Hasnomedicalproblems.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnomedicalproblems
		{
			get { return _entity.Hasnomedicalproblems; }
		}
        /// <summary>
        /// Gets the Hasnodlqi
        /// </summary>
        /// <value>The Hasnodlqi.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnodlqi
		{
			get { return _entity.Hasnodlqi; }
		}
        /// <summary>
        /// Gets the Hasnolifestylefactors
        /// </summary>
        /// <value>The Hasnolifestylefactors.</value>
		[System.ComponentModel.Bindable(true)]
		public bool Hasnolifestylefactors
		{
			get { return _entity.Hasnolifestylefactors; }
		}
        /// <summary>
        /// Gets the Cageinapplicable
        /// </summary>
        /// <value>The Cageinapplicable.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Cageinapplicable
		{
			get { return _entity.Cageinapplicable; }
		}
        /// <summary>
        /// Gets the Haqinapplicable
        /// </summary>
        /// <value>The Haqinapplicable.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Haqinapplicable
		{
			get { return _entity.Haqinapplicable; }
		}
        /// <summary>
        /// Gets the Discontinuedbiotherapy
        /// </summary>
        /// <value>The Discontinuedbiotherapy.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Discontinuedbiotherapy
		{
			get { return _entity.Discontinuedbiotherapy; }
		}
        /// <summary>
        /// Gets the Checkedbyid
        /// </summary>
        /// <value>The Checkedbyid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Checkedbyid
		{
			get { return _entity.Checkedbyid; }
		}
        /// <summary>
        /// Gets the Checkedbydate
        /// </summary>
        /// <value>The Checkedbydate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Checkedbydate
		{
			get { return _entity.Checkedbydate; }
		}
        /// <summary>
        /// Gets the Haspreviousantipsoriaticdrugs
        /// </summary>
        /// <value>The Haspreviousantipsoriaticdrugs.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Haspreviousantipsoriaticdrugs
		{
			get { return _entity.Haspreviousantipsoriaticdrugs; }
		}
        /// <summary>
        /// Gets the Haschangedbiologictherapy
        /// </summary>
        /// <value>The Haschangedbiologictherapy.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Haschangedbiologictherapy
		{
			get { return _entity.Haschangedbiologictherapy; }
		}
        /// <summary>
        /// Gets the Hasnewadverseevents
        /// </summary>
        /// <value>The Hasnewadverseevents.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Hasnewadverseevents
		{
			get { return _entity.Hasnewadverseevents; }
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
        /// Gets the Haspsoriaticarthiritis
        /// </summary>
        /// <value>The Haspsoriaticarthiritis.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Haspsoriaticarthiritis
		{
			get { return _entity.Haspsoriaticarthiritis; }
		}
        /// <summary>
        /// Gets the Hasinflamatoryarthiritis
        /// </summary>
        /// <value>The Hasinflamatoryarthiritis.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? Hasinflamatoryarthiritis
		{
			get { return _entity.Hasinflamatoryarthiritis; }
		}
        /// <summary>
        /// Gets the Psoriaticarthiritisonset
        /// </summary>
        /// <value>The Psoriaticarthiritisonset.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Psoriaticarthiritisonset
		{
			get { return _entity.Psoriaticarthiritisonset; }
		}
        /// <summary>
        /// Gets the TruncatedFupApplicable
        /// </summary>
        /// <value>The TruncatedFupApplicable.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? TruncatedFupApplicable
		{
			get { return _entity.TruncatedFupApplicable; }
		}
        /// <summary>
        /// Gets the Psoriaticarthiritisonsetdate
        /// </summary>
        /// <value>The Psoriaticarthiritisonsetdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Psoriaticarthiritisonsetdate
		{
			get { return _entity.Psoriaticarthiritisonsetdate; }
		}
        /// <summary>
        /// Gets the HasnoSmiTherapy
        /// </summary>
        /// <value>The HasnoSmiTherapy.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? HasnoSmiTherapy
		{
			get { return _entity.HasnoSmiTherapy; }
		}
        /// <summary>
        /// Gets the ClinicAttendance
        /// </summary>
        /// <value>The ClinicAttendance.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? ClinicAttendance
		{
			get { return _entity.ClinicAttendance; }
		}

        /// <summary>
        /// Gets a <see cref="T:MHS.Badbir.NetTiers.Entities.BbPatientCohortTracking"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MHS.Badbir.NetTiers.Entities.BbPatientCohortTracking Entity
        {
            get { return _entity; }
        }
	}
}
