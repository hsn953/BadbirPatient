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
    /// A designer class for a strongly typed repeater <c>BbCentreRepeater</c>
    /// </summary>
	public class BbCentreRepeaterDesigner : System.Web.UI.Design.ControlDesigner
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbCentreRepeaterDesigner"/> class.
        /// </summary>
		public BbCentreRepeaterDesigner()
		{
		}

        /// <summary>
        /// Initializes the control designer and loads the specified component.
        /// </summary>
        /// <param name="component">The control being designed.</param>
		public override void Initialize(IComponent component)
		{
			if (!(component is BbCentreRepeater))
			{ 
				throw new ArgumentException("Component is not a BbCentreRepeater."); 
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
			BbCentreRepeater z = (BbCentreRepeater)Component;
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
    /// A strongly typed repeater control for the <see cref="BbCentreRepeater"/> Type.
    /// </summary>
	[Designer(typeof(BbCentreRepeaterDesigner))]
	[ParseChildren(true)]
	[ToolboxData("<{0}:BbCentreRepeater runat=\"server\"></{0}:BbCentreRepeater>")]
	public class BbCentreRepeater : CompositeDataBoundControl, System.Web.UI.INamingContainer
	{
	    /// <summary>
        /// Initializes a new instance of the <see cref="T:BbCentreRepeater"/> class.
        /// </summary>
		public BbCentreRepeater()
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
		[TemplateContainer(typeof(BbCentreItem))]
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
		[TemplateContainer(typeof(BbCentreItem))]
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
        [TemplateContainer(typeof(BbCentreItem))]
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
		[TemplateContainer(typeof(BbCentreItem))]
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
		[TemplateContainer(typeof(BbCentreItem))]
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
						MHS.Badbir.NetTiers.Entities.BbCentre entity = o as MHS.Badbir.NetTiers.Entities.BbCentre;
						BbCentreItem container = new BbCentreItem(entity);
	
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
	public class BbCentreItem : System.Web.UI.Control, System.Web.UI.INamingContainer
	{
		private MHS.Badbir.NetTiers.Entities.BbCentre _entity;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbCentreItem"/> class.
        /// </summary>
		public BbCentreItem()
			: base()
		{ }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:BbCentreItem"/> class.
        /// </summary>
		public BbCentreItem(MHS.Badbir.NetTiers.Entities.BbCentre entity)
			: base()
		{
			_entity = entity;
		}
		
        /// <summary>
        /// Gets the Centreid
        /// </summary>
        /// <value>The Centreid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32 Centreid
		{
			get { return _entity.Centreid; }
		}
        /// <summary>
        /// Gets the Centrename
        /// </summary>
        /// <value>The Centrename.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Centrename
		{
			get { return _entity.Centrename; }
		}
        /// <summary>
        /// Gets the Address1
        /// </summary>
        /// <value>The Address1.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Address1
		{
			get { return _entity.Address1; }
		}
        /// <summary>
        /// Gets the Address2
        /// </summary>
        /// <value>The Address2.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Address2
		{
			get { return _entity.Address2; }
		}
        /// <summary>
        /// Gets the Address3
        /// </summary>
        /// <value>The Address3.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Address3
		{
			get { return _entity.Address3; }
		}
        /// <summary>
        /// Gets the Address4
        /// </summary>
        /// <value>The Address4.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Address4
		{
			get { return _entity.Address4; }
		}
        /// <summary>
        /// Gets the Address5
        /// </summary>
        /// <value>The Address5.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Address5
		{
			get { return _entity.Address5; }
		}
        /// <summary>
        /// Gets the Postcode
        /// </summary>
        /// <value>The Postcode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Postcode
		{
			get { return _entity.Postcode; }
		}
        /// <summary>
        /// Gets the Mapref
        /// </summary>
        /// <value>The Mapref.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Mapref
		{
			get { return _entity.Mapref; }
		}
        /// <summary>
        /// Gets the Centreregionid
        /// </summary>
        /// <value>The Centreregionid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Centreregionid
		{
			get { return _entity.Centreregionid; }
		}
        /// <summary>
        /// Gets the Centrestatusid
        /// </summary>
        /// <value>The Centrestatusid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Centrestatusid
		{
			get { return _entity.Centrestatusid; }
		}
        /// <summary>
        /// Gets the Piid
        /// </summary>
        /// <value>The Piid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? Piid
		{
			get { return _entity.Piid; }
		}
        /// <summary>
        /// Gets the Picontactdate
        /// </summary>
        /// <value>The Picontactdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Picontactdate
		{
			get { return _entity.Picontactdate; }
		}
        /// <summary>
        /// Gets the Ssisubmittedrddate
        /// </summary>
        /// <value>The Ssisubmittedrddate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Ssisubmittedrddate
		{
			get { return _entity.Ssisubmittedrddate; }
		}
        /// <summary>
        /// Gets the Lrecapproveddate
        /// </summary>
        /// <value>The Lrecapproveddate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Lrecapproveddate
		{
			get { return _entity.Lrecapproveddate; }
		}
        /// <summary>
        /// Gets the Rdapproveddate
        /// </summary>
        /// <value>The Rdapproveddate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Rdapproveddate
		{
			get { return _entity.Rdapproveddate; }
		}
        /// <summary>
        /// Gets the Setupdate
        /// </summary>
        /// <value>The Setupdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Setupdate
		{
			get { return _entity.Setupdate; }
		}
        /// <summary>
        /// Gets the Pat1recruiteddate
        /// </summary>
        /// <value>The Pat1recruiteddate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Pat1recruiteddate
		{
			get { return _entity.Pat1recruiteddate; }
		}
        /// <summary>
        /// Gets the Financecontact
        /// </summary>
        /// <value>The Financecontact.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Financecontact
		{
			get { return _entity.Financecontact; }
		}
        /// <summary>
        /// Gets the Contactdetails
        /// </summary>
        /// <value>The Contactdetails.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Contactdetails
		{
			get { return _entity.Contactdetails; }
		}
        /// <summary>
        /// Gets the Accountnumber
        /// </summary>
        /// <value>The Accountnumber.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Accountnumber
		{
			get { return _entity.Accountnumber; }
		}
        /// <summary>
        /// Gets the ClrNstatus
        /// </summary>
        /// <value>The ClrNstatus.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String ClrNstatus
		{
			get { return _entity.ClrNstatus; }
		}
        /// <summary>
        /// Gets the UkcrNregionid
        /// </summary>
        /// <value>The UkcrNregionid.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? UkcrNregionid
		{
			get { return _entity.UkcrNregionid; }
		}
        /// <summary>
        /// Gets the UkcrnContact
        /// </summary>
        /// <value>The UkcrnContact.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String UkcrnContact
		{
			get { return _entity.UkcrnContact; }
		}
        /// <summary>
        /// Gets the UkcrnSitecode
        /// </summary>
        /// <value>The UkcrnSitecode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String UkcrnSitecode
		{
			get { return _entity.UkcrnSitecode; }
		}
        /// <summary>
        /// Gets the UkcrnSitenumber
        /// </summary>
        /// <value>The UkcrnSitenumber.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String UkcrnSitenumber
		{
			get { return _entity.UkcrnSitenumber; }
		}
        /// <summary>
        /// Gets the SkipNhsNumber
        /// </summary>
        /// <value>The SkipNhsNumber.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Boolean? SkipNhsNumber
		{
			get { return _entity.SkipNhsNumber; }
		}
        /// <summary>
        /// Gets the Comments
        /// </summary>
        /// <value>The Comments.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Comments
		{
			get { return _entity.Comments; }
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
        /// Gets the Primarycontact
        /// </summary>
        /// <value>The Primarycontact.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Primarycontact
		{
			get { return _entity.Primarycontact; }
		}
        /// <summary>
        /// Gets the Picontact
        /// </summary>
        /// <value>The Picontact.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Picontact
		{
			get { return _entity.Picontact; }
		}
        /// <summary>
        /// Gets the PiidName
        /// </summary>
        /// <value>The PiidName.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String PiidName
		{
			get { return _entity.PiidName; }
		}
        /// <summary>
        /// Gets the Rndcontact
        /// </summary>
        /// <value>The Rndcontact.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Rndcontact
		{
			get { return _entity.Rndcontact; }
		}
        /// <summary>
        /// Gets the Rndreference
        /// </summary>
        /// <value>The Rndreference.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Rndreference
		{
			get { return _entity.Rndreference; }
		}
        /// <summary>
        /// Gets the Teamadditioncomments
        /// </summary>
        /// <value>The Teamadditioncomments.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Teamadditioncomments
		{
			get { return _entity.Teamadditioncomments; }
		}
        /// <summary>
        /// Gets the Auditstatus
        /// </summary>
        /// <value>The Auditstatus.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Auditstatus
		{
			get { return _entity.Auditstatus; }
		}
        /// <summary>
        /// Gets the Auditedby
        /// </summary>
        /// <value>The Auditedby.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Auditedby
		{
			get { return _entity.Auditedby; }
		}
        /// <summary>
        /// Gets the Auditnotes
        /// </summary>
        /// <value>The Auditnotes.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Auditnotes
		{
			get { return _entity.Auditnotes; }
		}
        /// <summary>
        /// Gets the Amd7Sentdate
        /// </summary>
        /// <value>The Amd7Sentdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd7Sentdate
		{
			get { return _entity.Amd7Sentdate; }
		}
        /// <summary>
        /// Gets the Amd7Approvaldate
        /// </summary>
        /// <value>The Amd7Approvaldate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd7Approvaldate
		{
			get { return _entity.Amd7Approvaldate; }
		}
        /// <summary>
        /// Gets the Ctamdaug13Sentdate
        /// </summary>
        /// <value>The Ctamdaug13Sentdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Ctamdaug13Sentdate
		{
			get { return _entity.Ctamdaug13Sentdate; }
		}
        /// <summary>
        /// Gets the Ctamdaug13Approvaldate
        /// </summary>
        /// <value>The Ctamdaug13Approvaldate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Ctamdaug13Approvaldate
		{
			get { return _entity.Ctamdaug13Approvaldate; }
		}
        /// <summary>
        /// Gets the OtherActiveUsers
        /// </summary>
        /// <value>The OtherActiveUsers.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String OtherActiveUsers
		{
			get { return _entity.OtherActiveUsers; }
		}
        /// <summary>
        /// Gets the Audit2status
        /// </summary>
        /// <value>The Audit2status.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit2status
		{
			get { return _entity.Audit2status; }
		}
        /// <summary>
        /// Gets the Audit2by
        /// </summary>
        /// <value>The Audit2by.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit2by
		{
			get { return _entity.Audit2by; }
		}
        /// <summary>
        /// Gets the Audit2notes
        /// </summary>
        /// <value>The Audit2notes.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit2notes
		{
			get { return _entity.Audit2notes; }
		}
        /// <summary>
        /// Gets the Audit3status
        /// </summary>
        /// <value>The Audit3status.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit3status
		{
			get { return _entity.Audit3status; }
		}
        /// <summary>
        /// Gets the Audit3by
        /// </summary>
        /// <value>The Audit3by.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit3by
		{
			get { return _entity.Audit3by; }
		}
        /// <summary>
        /// Gets the Audit3notes
        /// </summary>
        /// <value>The Audit3notes.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit3notes
		{
			get { return _entity.Audit3notes; }
		}
        /// <summary>
        /// Gets the Audit4status
        /// </summary>
        /// <value>The Audit4status.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit4status
		{
			get { return _entity.Audit4status; }
		}
        /// <summary>
        /// Gets the Audit4by
        /// </summary>
        /// <value>The Audit4by.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit4by
		{
			get { return _entity.Audit4by; }
		}
        /// <summary>
        /// Gets the Audit4notes
        /// </summary>
        /// <value>The Audit4notes.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit4notes
		{
			get { return _entity.Audit4notes; }
		}
        /// <summary>
        /// Gets the Audit1Date
        /// </summary>
        /// <value>The Audit1Date.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit1Date
		{
			get { return _entity.Audit1Date; }
		}
        /// <summary>
        /// Gets the Audit2Date
        /// </summary>
        /// <value>The Audit2Date.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit2Date
		{
			get { return _entity.Audit2Date; }
		}
        /// <summary>
        /// Gets the Audit3Date
        /// </summary>
        /// <value>The Audit3Date.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit3Date
		{
			get { return _entity.Audit3Date; }
		}
        /// <summary>
        /// Gets the Audit4Date
        /// </summary>
        /// <value>The Audit4Date.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit4Date
		{
			get { return _entity.Audit4Date; }
		}
        /// <summary>
        /// Gets the Audit1RptSentDate
        /// </summary>
        /// <value>The Audit1RptSentDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit1RptSentDate
		{
			get { return _entity.Audit1RptSentDate; }
		}
        /// <summary>
        /// Gets the Audit2RptSentDate
        /// </summary>
        /// <value>The Audit2RptSentDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit2RptSentDate
		{
			get { return _entity.Audit2RptSentDate; }
		}
        /// <summary>
        /// Gets the Audit3RptSentDate
        /// </summary>
        /// <value>The Audit3RptSentDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit3RptSentDate
		{
			get { return _entity.Audit3RptSentDate; }
		}
        /// <summary>
        /// Gets the Audit4RptSentDate
        /// </summary>
        /// <value>The Audit4RptSentDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit4RptSentDate
		{
			get { return _entity.Audit4RptSentDate; }
		}
        /// <summary>
        /// Gets the Audit1RptRcvdDate
        /// </summary>
        /// <value>The Audit1RptRcvdDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit1RptRcvdDate
		{
			get { return _entity.Audit1RptRcvdDate; }
		}
        /// <summary>
        /// Gets the Audit2RptRcvdDate
        /// </summary>
        /// <value>The Audit2RptRcvdDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit2RptRcvdDate
		{
			get { return _entity.Audit2RptRcvdDate; }
		}
        /// <summary>
        /// Gets the Audit3RptRcvdDate
        /// </summary>
        /// <value>The Audit3RptRcvdDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit3RptRcvdDate
		{
			get { return _entity.Audit3RptRcvdDate; }
		}
        /// <summary>
        /// Gets the Audit4RptRcvdDate
        /// </summary>
        /// <value>The Audit4RptRcvdDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit4RptRcvdDate
		{
			get { return _entity.Audit4RptRcvdDate; }
		}
        /// <summary>
        /// Gets the Amd8Sentdate
        /// </summary>
        /// <value>The Amd8Sentdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd8Sentdate
		{
			get { return _entity.Amd8Sentdate; }
		}
        /// <summary>
        /// Gets the Amd8Approvaldate
        /// </summary>
        /// <value>The Amd8Approvaldate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd8Approvaldate
		{
			get { return _entity.Amd8Approvaldate; }
		}
        /// <summary>
        /// Gets the Amd9Sentdate
        /// </summary>
        /// <value>The Amd9Sentdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd9Sentdate
		{
			get { return _entity.Amd9Sentdate; }
		}
        /// <summary>
        /// Gets the Amd9Approvaldate
        /// </summary>
        /// <value>The Amd9Approvaldate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd9Approvaldate
		{
			get { return _entity.Amd9Approvaldate; }
		}
        /// <summary>
        /// Gets the AltCentreId
        /// </summary>
        /// <value>The AltCentreId.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String AltCentreId
		{
			get { return _entity.AltCentreId; }
		}
        /// <summary>
        /// Gets the IsTrainingCentre
        /// </summary>
        /// <value>The IsTrainingCentre.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Int32? IsTrainingCentre
		{
			get { return _entity.IsTrainingCentre; }
		}
        /// <summary>
        /// Gets the Audit5status
        /// </summary>
        /// <value>The Audit5status.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit5status
		{
			get { return _entity.Audit5status; }
		}
        /// <summary>
        /// Gets the Audit5by
        /// </summary>
        /// <value>The Audit5by.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit5by
		{
			get { return _entity.Audit5by; }
		}
        /// <summary>
        /// Gets the Audit5notes
        /// </summary>
        /// <value>The Audit5notes.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit5notes
		{
			get { return _entity.Audit5notes; }
		}
        /// <summary>
        /// Gets the Audit5Date
        /// </summary>
        /// <value>The Audit5Date.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit5Date
		{
			get { return _entity.Audit5Date; }
		}
        /// <summary>
        /// Gets the Audit5RptSentDate
        /// </summary>
        /// <value>The Audit5RptSentDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit5RptSentDate
		{
			get { return _entity.Audit5RptSentDate; }
		}
        /// <summary>
        /// Gets the Audit5RptRcvdDate
        /// </summary>
        /// <value>The Audit5RptRcvdDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit5RptRcvdDate
		{
			get { return _entity.Audit5RptRcvdDate; }
		}
        /// <summary>
        /// Gets the Audit6status
        /// </summary>
        /// <value>The Audit6status.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit6status
		{
			get { return _entity.Audit6status; }
		}
        /// <summary>
        /// Gets the Audit6by
        /// </summary>
        /// <value>The Audit6by.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit6by
		{
			get { return _entity.Audit6by; }
		}
        /// <summary>
        /// Gets the Audit6notes
        /// </summary>
        /// <value>The Audit6notes.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit6notes
		{
			get { return _entity.Audit6notes; }
		}
        /// <summary>
        /// Gets the Audit6Date
        /// </summary>
        /// <value>The Audit6Date.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit6Date
		{
			get { return _entity.Audit6Date; }
		}
        /// <summary>
        /// Gets the Audit6RptSentDate
        /// </summary>
        /// <value>The Audit6RptSentDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit6RptSentDate
		{
			get { return _entity.Audit6RptSentDate; }
		}
        /// <summary>
        /// Gets the Audit6RptRcvdDate
        /// </summary>
        /// <value>The Audit6RptRcvdDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit6RptRcvdDate
		{
			get { return _entity.Audit6RptRcvdDate; }
		}
        /// <summary>
        /// Gets the Audit7status
        /// </summary>
        /// <value>The Audit7status.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit7status
		{
			get { return _entity.Audit7status; }
		}
        /// <summary>
        /// Gets the Audit7by
        /// </summary>
        /// <value>The Audit7by.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit7by
		{
			get { return _entity.Audit7by; }
		}
        /// <summary>
        /// Gets the Audit7notes
        /// </summary>
        /// <value>The Audit7notes.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit7notes
		{
			get { return _entity.Audit7notes; }
		}
        /// <summary>
        /// Gets the Audit7Date
        /// </summary>
        /// <value>The Audit7Date.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit7Date
		{
			get { return _entity.Audit7Date; }
		}
        /// <summary>
        /// Gets the Audit7RptSentDate
        /// </summary>
        /// <value>The Audit7RptSentDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit7RptSentDate
		{
			get { return _entity.Audit7RptSentDate; }
		}
        /// <summary>
        /// Gets the Audit7RptRcvdDate
        /// </summary>
        /// <value>The Audit7RptRcvdDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit7RptRcvdDate
		{
			get { return _entity.Audit7RptRcvdDate; }
		}
        /// <summary>
        /// Gets the Audit8status
        /// </summary>
        /// <value>The Audit8status.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit8status
		{
			get { return _entity.Audit8status; }
		}
        /// <summary>
        /// Gets the Audit8by
        /// </summary>
        /// <value>The Audit8by.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit8by
		{
			get { return _entity.Audit8by; }
		}
        /// <summary>
        /// Gets the Audit8notes
        /// </summary>
        /// <value>The Audit8notes.</value>
		[System.ComponentModel.Bindable(true)]
		public System.String Audit8notes
		{
			get { return _entity.Audit8notes; }
		}
        /// <summary>
        /// Gets the Audit8Date
        /// </summary>
        /// <value>The Audit8Date.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit8Date
		{
			get { return _entity.Audit8Date; }
		}
        /// <summary>
        /// Gets the Audit8RptSentDate
        /// </summary>
        /// <value>The Audit8RptSentDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit8RptSentDate
		{
			get { return _entity.Audit8RptSentDate; }
		}
        /// <summary>
        /// Gets the Audit8RptRcvdDate
        /// </summary>
        /// <value>The Audit8RptRcvdDate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Audit8RptRcvdDate
		{
			get { return _entity.Audit8RptRcvdDate; }
		}
        /// <summary>
        /// Gets the Amd10Sentdate
        /// </summary>
        /// <value>The Amd10Sentdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd10Sentdate
		{
			get { return _entity.Amd10Sentdate; }
		}
        /// <summary>
        /// Gets the Amd10Approvaldate
        /// </summary>
        /// <value>The Amd10Approvaldate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd10Approvaldate
		{
			get { return _entity.Amd10Approvaldate; }
		}
        /// <summary>
        /// Gets the Amd11Sentdate
        /// </summary>
        /// <value>The Amd11Sentdate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd11Sentdate
		{
			get { return _entity.Amd11Sentdate; }
		}
        /// <summary>
        /// Gets the Amd11Approvaldate
        /// </summary>
        /// <value>The Amd11Approvaldate.</value>
		[System.ComponentModel.Bindable(true)]
		public System.DateTime? Amd11Approvaldate
		{
			get { return _entity.Amd11Approvaldate; }
		}

        /// <summary>
        /// Gets a <see cref="T:MHS.Badbir.NetTiers.Entities.BbCentre"></see> object
        /// </summary>
        /// <value></value>
        [System.ComponentModel.Bindable(true)]
        public MHS.Badbir.NetTiers.Entities.BbCentre Entity
        {
            get { return _entity; }
        }
	}
}
