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
    /// A designer class for a strongly typed repeater <c>BbPatientRepeater</c>
    /// </summary>
	public class BbPatientRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientRepeaterDesigner"/> class.
        /// </summary>
		public BbPatientRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is BbPatientRepeater))
			{ 
				throw new ArgumentException("Component is not a BbPatientRepeater."); 
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
			BbPatientRepeater z = (BbPatientRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="BbPatientRepeater"/> Type.
    /// </summary>
	[Designer(typeof(BbPatientRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:BbPatientRepeater runat=\"server\"></{0}:BbPatientRepeater>")]
	public class BbPatientRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientRepeater"/> class.
        /// </summary>
		public BbPatientRepeater()
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
		[TemplateContainer(typeof(BbPatientItem))]
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
		[TemplateContainer(typeof(BbPatientItem))]
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
        [TemplateContainer(typeof(BbPatientItem))]
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
		[TemplateContainer(typeof(BbPatientItem))]
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
		[TemplateContainer(typeof(BbPatientItem))]
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
						MHS.Badbir.NetTiers.Entities.BbPatient entity = o as MHS.Badbir.NetTiers.Entities.BbPatient;
						BbPatientItem container = new BbPatientItem(entity);
	
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
	public class BbPatientItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MHS.Badbir.NetTiers.Entities.BbPatient _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientItem"/> class.
        /// </summary>
		public BbPatientItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbPatientItem"/> class.
        /// </summary>
		public BbPatientItem(MHS.Badbir.NetTiers.Entities.BbPatient entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the Patientid
        /// </summary>
        /// <value>The Patientid.</value>
		[System.ComponentModel.Bindable(true)]
		public int Patientid
		{
			get { return _entity.Patientid; }
		}
        /// <summary>
        /// Gets the Firststudyno
        /// </summary>
        /// <value>The Firststudyno.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Firststudyno
		{
			get { return _entity.Firststudyno; }
		}
        /// <summary>
        /// Gets the Studyidlastfive
        /// </summary>
        /// <value>The Studyidlastfive.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Studyidlastfive
		{
			get { return _entity.Studyidlastfive; }
		}
        /// <summary>
        /// Gets the Dateconsented
        /// </summary>
        /// <value>The Dateconsented.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Dateconsented
		{
			get { return _entity.Dateconsented; }
		}
        /// <summary>
        /// Gets the Consentformreceived
        /// </summary>
        /// <value>The Consentformreceived.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Consentformreceived
		{
			get { return _entity.Consentformreceived; }
		}
        /// <summary>
        /// Gets the Phrn
        /// </summary>
        /// <value>The Phrn.</value>
		[System.ComponentModel.Bindable(true)]
		public string Phrn
		{
			get { return _entity.Phrn; }
		}
        /// <summary>
        /// Gets the Pnhs
        /// </summary>
        /// <value>The Pnhs.</value>
		[System.ComponentModel.Bindable(true)]
		public string Pnhs
		{
			get { return _entity.Pnhs; }
		}
        /// <summary>
        /// Gets the Genderid
        /// </summary>
        /// <value>The Genderid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Genderid
		{
			get { return _entity.Genderid; }
		}
        /// <summary>
        /// Gets the Dateofbirth
        /// </summary>
        /// <value>The Dateofbirth.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Dateofbirth
		{
			get { return _entity.Dateofbirth; }
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
        /// Gets the Surname
        /// </summary>
        /// <value>The Surname.</value>
		[System.ComponentModel.Bindable(true)]
		public string Surname
		{
			get { return _entity.Surname; }
		}
        /// <summary>
        /// Gets the Forenames
        /// </summary>
        /// <value>The Forenames.</value>
		[System.ComponentModel.Bindable(true)]
		public string Forenames
		{
			get { return _entity.Forenames; }
		}
        /// <summary>
        /// Gets the Address1
        /// </summary>
        /// <value>The Address1.</value>
		[System.ComponentModel.Bindable(true)]
		public string Address1
		{
			get { return _entity.Address1; }
		}
        /// <summary>
        /// Gets the Address2
        /// </summary>
        /// <value>The Address2.</value>
		[System.ComponentModel.Bindable(true)]
		public string Address2
		{
			get { return _entity.Address2; }
		}
        /// <summary>
        /// Gets the Address3
        /// </summary>
        /// <value>The Address3.</value>
		[System.ComponentModel.Bindable(true)]
		public string Address3
		{
			get { return _entity.Address3; }
		}
        /// <summary>
        /// Gets the AddressTown
        /// </summary>
        /// <value>The AddressTown.</value>
		[System.ComponentModel.Bindable(true)]
		public string AddressTown
		{
			get { return _entity.AddressTown; }
		}
        /// <summary>
        /// Gets the AddressCounty
        /// </summary>
        /// <value>The AddressCounty.</value>
		[System.ComponentModel.Bindable(true)]
		public string AddressCounty
		{
			get { return _entity.AddressCounty; }
		}
        /// <summary>
        /// Gets the AddressPostcode
        /// </summary>
        /// <value>The AddressPostcode.</value>
		[System.ComponentModel.Bindable(true)]
		public string AddressPostcode
		{
			get { return _entity.AddressPostcode; }
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
        /// Gets the Emailaddress
        /// </summary>
        /// <value>The Emailaddress.</value>
		[System.ComponentModel.Bindable(true)]
		public string Emailaddress
		{
			get { return _entity.Emailaddress; }
		}
        /// <summary>
        /// Gets the Countryresidence
        /// </summary>
        /// <value>The Countryresidence.</value>
		[System.ComponentModel.Bindable(true)]
		public string Countryresidence
		{
			get { return _entity.Countryresidence; }
		}
        /// <summary>
        /// Gets the Statusid
        /// </summary>
        /// <value>The Statusid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Statusid
		{
			get { return _entity.Statusid; }
		}
        /// <summary>
        /// Gets the LosttoFup
        /// </summary>
        /// <value>The LosttoFup.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? LosttoFup
		{
			get { return _entity.LosttoFup; }
		}
        /// <summary>
        /// Gets the Registrationcohortid
        /// </summary>
        /// <value>The Registrationcohortid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Registrationcohortid
		{
			get { return _entity.Registrationcohortid; }
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
        /// Gets the Tempstudyno
        /// </summary>
        /// <value>The Tempstudyno.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Tempstudyno
		{
			get { return _entity.Tempstudyno; }
		}
        /// <summary>
        /// Gets the Consentversion
        /// </summary>
        /// <value>The Consentversion.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Double? Consentversion
		{
			get { return _entity.Consentversion; }
		}
        /// <summary>
        /// Gets the Statusdetailid
        /// </summary>
        /// <value>The Statusdetailid.</value>
		[System.ComponentModel.Bindable(true)]
		public int Statusdetailid
		{
			get { return _entity.Statusdetailid; }
		}
        /// <summary>
        /// Gets the Consentfileaddress
        /// </summary>
        /// <value>The Consentfileaddress.</value>
		[System.ComponentModel.Bindable(true)]
		public string Consentfileaddress
		{
			get { return _entity.Consentfileaddress; }
		}
        /// <summary>
        /// Gets the Deathdate
        /// </summary>
        /// <value>The Deathdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Deathdate
		{
			get { return _entity.Deathdate; }
		}
        /// <summary>
        /// Gets the AltStudyId234Digits
        /// </summary>
        /// <value>The AltStudyId234Digits.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? AltStudyId234Digits
		{
			get { return _entity.AltStudyId234Digits; }
		}
        /// <summary>
        /// Gets the AltDeathDate
        /// </summary>
        /// <value>The AltDeathDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? AltDeathDate
		{
			get { return _entity.AltDeathDate; }
		}
        /// <summary>
        /// Gets the ConsentedByBadbirUserId
        /// </summary>
        /// <value>The ConsentedByBadbirUserId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? ConsentedByBadbirUserId
		{
			get { return _entity.ConsentedByBadbirUserId; }
		}

        /// <summary>
        /// Gets a <see cref="T:MHS.Badbir.NetTiers.Entities.BbPatient"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MHS.Badbir.NetTiers.Entities.BbPatient Entity
        {
            get { return _entity; }
        }
	}
}
