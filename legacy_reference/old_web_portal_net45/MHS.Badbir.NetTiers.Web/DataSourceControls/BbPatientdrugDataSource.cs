#region Using Directives
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Web.UI;
using System.Web.UI.Design;

using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Data;
using MHS.Badbir.NetTiers.Data.Bases;
using MHS.Badbir.NetTiers.Component;
#endregion

namespace MHS.Badbir.NetTiers.Web.Data
{
	/// <summary>
	/// Represents the DataRepository.BbPatientdrugProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientdrugDataSourceDesigner))]
	public class BbPatientdrugDataSource : ProviderDataSource<BbPatientdrug, BbPatientdrugKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugDataSource class.
		/// </summary>
		public BbPatientdrugDataSource() : base(new BbPatientdrugService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientdrugDataSourceView used by the BbPatientdrugDataSource.
		/// </summary>
		protected BbPatientdrugDataSourceView BbPatientdrugView
		{
			get { return ( View as BbPatientdrugDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientdrugDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientdrugSelectMethod SelectMethod
		{
			get
			{
				BbPatientdrugSelectMethod selectMethod = BbPatientdrugSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientdrugSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientdrugDataSourceView class that is to be
		/// used by the BbPatientdrugDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientdrugDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientdrug, BbPatientdrugKey> GetNewDataSourceView()
		{
			return new BbPatientdrugDataSourceView(this, DefaultViewName);
		}
		
		/// <summary>
        /// Creates a cache hashing key based on the startIndex, pageSize and the SelectMethod being used.
        /// </summary>
        /// <param name="startIndex">The current start row index.</param>
        /// <param name="pageSize">The current page size.</param>
        /// <returns>A string that can be used as a key for caching purposes.</returns>
		protected override string CacheHashKey(int startIndex, int pageSize)
        {
			return String.Format("{0}:{1}:{2}", SelectMethod, startIndex, pageSize);
        }
		
		#endregion Methods
	}
	
	/// <summary>
	/// Supports the BbPatientdrugDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientdrugDataSourceView : ProviderDataSourceView<BbPatientdrug, BbPatientdrugKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientdrugDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientdrugDataSourceView(BbPatientdrugDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientdrugDataSource BbPatientdrugOwner
		{
			get { return Owner as BbPatientdrugDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientdrugSelectMethod SelectMethod
		{
			get { return BbPatientdrugOwner.SelectMethod; }
			set { BbPatientdrugOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientdrugService BbPatientdrugProvider
		{
			get { return Provider as BbPatientdrugService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientdrug> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientdrug> results = null;
			BbPatientdrug item;
			count = 0;
			
			int _fupId;
			System.Int32? _startyear_nullable;
			System.Int32? _startmonth_nullable;
			System.Int32? _startday_nullable;
			System.Int32? _stopyear_nullable;
			System.Int32? _stopmonth_nullable;
			System.Int32? _stopday_nullable;
			int _patdrugid;
			System.Int32? _commonfrequencyid_nullable;
			System.Int32? _doseunitid_nullable;
			int _drugid;
			System.Int32? _stopreasonid_nullable;
			System.Int32? sp197_FupId;
			System.Int32? sp198_FupId;
			System.Int32? sp200_FupId;
			System.Int32? sp195_FupId;
			System.Int32? sp194_Fupaeid;
			System.Int32? sp196_FupId;
			System.Int32? sp199_FupId;

			switch ( SelectMethod )
			{
				case BbPatientdrugSelectMethod.Get:
					BbPatientdrugKey entityKey  = new BbPatientdrugKey();
					entityKey.Load(values);
					item = BbPatientdrugProvider.Get(entityKey);
					results = new TList<BbPatientdrug>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientdrugSelectMethod.GetAll:
                    results = BbPatientdrugProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientdrugSelectMethod.GetPaged:
					results = BbPatientdrugProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientdrugSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientdrugProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientdrugProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientdrugSelectMethod.GetByPatdrugid:
					_patdrugid = ( values["Patdrugid"] != null ) ? (int) EntityUtil.ChangeType(values["Patdrugid"], typeof(int)) : (int)0;
					item = BbPatientdrugProvider.GetByPatdrugid(_patdrugid);
					results = new TList<BbPatientdrug>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbPatientdrugSelectMethod.GetByFupId:
					_fupId = ( values["FupId"] != null ) ? (int) EntityUtil.ChangeType(values["FupId"], typeof(int)) : (int)0;
					results = BbPatientdrugProvider.GetByFupId(_fupId, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientdrugSelectMethod.GetByStartyearStartmonthStartdayStopyearStopmonthStopday:
					_startyear_nullable = (System.Int32?) EntityUtil.ChangeType(values["Startyear"], typeof(System.Int32?));
					_startmonth_nullable = (System.Int32?) EntityUtil.ChangeType(values["Startmonth"], typeof(System.Int32?));
					_startday_nullable = (System.Int32?) EntityUtil.ChangeType(values["Startday"], typeof(System.Int32?));
					_stopyear_nullable = (System.Int32?) EntityUtil.ChangeType(values["Stopyear"], typeof(System.Int32?));
					_stopmonth_nullable = (System.Int32?) EntityUtil.ChangeType(values["Stopmonth"], typeof(System.Int32?));
					_stopday_nullable = (System.Int32?) EntityUtil.ChangeType(values["Stopday"], typeof(System.Int32?));
					results = BbPatientdrugProvider.GetByStartyearStartmonthStartdayStopyearStopmonthStopday(_startyear_nullable, _startmonth_nullable, _startday_nullable, _stopyear_nullable, _stopmonth_nullable, _stopday_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientdrugSelectMethod.GetByStopyearStopmonthStopday:
					_stopyear_nullable = (System.Int32?) EntityUtil.ChangeType(values["Stopyear"], typeof(System.Int32?));
					_stopmonth_nullable = (System.Int32?) EntityUtil.ChangeType(values["Stopmonth"], typeof(System.Int32?));
					_stopday_nullable = (System.Int32?) EntityUtil.ChangeType(values["Stopday"], typeof(System.Int32?));
					results = BbPatientdrugProvider.GetByStopyearStopmonthStopday(_stopyear_nullable, _stopmonth_nullable, _stopday_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				case BbPatientdrugSelectMethod.GetByCommonfrequencyid:
					_commonfrequencyid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Commonfrequencyid"], typeof(System.Int32?));
					results = BbPatientdrugProvider.GetByCommonfrequencyid(_commonfrequencyid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientdrugSelectMethod.GetByDoseunitid:
					_doseunitid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Doseunitid"], typeof(System.Int32?));
					results = BbPatientdrugProvider.GetByDoseunitid(_doseunitid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientdrugSelectMethod.GetByDrugid:
					_drugid = ( values["Drugid"] != null ) ? (int) EntityUtil.ChangeType(values["Drugid"], typeof(int)) : (int)0;
					results = BbPatientdrugProvider.GetByDrugid(_drugid, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientdrugSelectMethod.GetByStopreasonid:
					_stopreasonid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Stopreasonid"], typeof(System.Int32?));
					results = BbPatientdrugProvider.GetByStopreasonid(_stopreasonid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				// Custom
				case BbPatientdrugSelectMethod.Patientdrug_GetByFupId_Conventional:
					sp197_FupId = (System.Int32?) EntityUtil.ChangeType(values["FupId"], typeof(System.Int32?));
					results = BbPatientdrugProvider.Patientdrug_GetByFupId_Conventional(sp197_FupId, StartIndex, PageSize);
					break;
				case BbPatientdrugSelectMethod.Patientdrug_GetByFupId_Current:
					sp198_FupId = (System.Int32?) EntityUtil.ChangeType(values["FupId"], typeof(System.Int32?));
					results = BbPatientdrugProvider.Patientdrug_GetByFupId_Current(sp198_FupId, StartIndex, PageSize);
					break;
				case BbPatientdrugSelectMethod.Patientdrug_GetByFupId_SmallMolecule:
					sp200_FupId = (System.Int32?) EntityUtil.ChangeType(values["FupId"], typeof(System.Int32?));
					results = BbPatientdrugProvider.Patientdrug_GetByFupId_SmallMolecule(sp200_FupId, StartIndex, PageSize);
					break;
				case BbPatientdrugSelectMethod.Patientdrug_GetByFupId_Biologic:
					sp195_FupId = (System.Int32?) EntityUtil.ChangeType(values["FupId"], typeof(System.Int32?));
					results = BbPatientdrugProvider.Patientdrug_GetByFupId_Biologic(sp195_FupId, StartIndex, PageSize);
					break;
				case BbPatientdrugSelectMethod.PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent:
					sp194_Fupaeid = (System.Int32?) EntityUtil.ChangeType(values["Fupaeid"], typeof(System.Int32?));
					results = BbPatientdrugProvider.PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent(sp194_Fupaeid, StartIndex, PageSize);
					break;
				case BbPatientdrugSelectMethod.Patientdrug_GetByFupId_Concomitant:
					sp196_FupId = (System.Int32?) EntityUtil.ChangeType(values["FupId"], typeof(System.Int32?));
					results = BbPatientdrugProvider.Patientdrug_GetByFupId_Concomitant(sp196_FupId, StartIndex, PageSize);
					break;
				case BbPatientdrugSelectMethod.Patientdrug_GetByFupId_Previous:
					sp199_FupId = (System.Int32?) EntityUtil.ChangeType(values["FupId"], typeof(System.Int32?));
					results = BbPatientdrugProvider.Patientdrug_GetByFupId_Previous(sp199_FupId, StartIndex, PageSize);
					break;
				default:
					break;
			}

			if ( results != null && count < 1 )
			{
				count = results.Count;

				if ( !String.IsNullOrEmpty(CustomMethodRecordCountParamName) )
				{
					object objCustomCount = EntityUtil.ChangeType(customOutput[CustomMethodRecordCountParamName], typeof(Int32));
					
					if ( objCustomCount != null )
					{
						count = (int) objCustomCount;
					}
				}
			}
			
			return results;
		}
		
		/// <summary>
		/// Gets the values of any supplied parameters for internal caching.
		/// </summary>
		/// <param name="values">An IDictionary object of name/value pairs.</param>
		protected override void GetSelectParameters(IDictionary values)
		{
			if ( SelectMethod == BbPatientdrugSelectMethod.Get || SelectMethod == BbPatientdrugSelectMethod.GetByPatdrugid )
			{
				EntityId = GetEntityKey(values);
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation for the current entity if it has
		/// not already been performed.
		/// </summary>
		internal override void DeepLoad()
		{
			if ( !IsDeepLoaded )
			{
				BbPatientdrug entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientdrugProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
					// set loaded flag
					IsDeepLoaded = true;
				}
			}
		}

		/// <summary>
		/// Performs a DeepLoad operation on the specified entity collection.
		/// </summary>
		/// <param name="entityList"></param>
		/// <param name="properties"></param>
		internal override void DeepLoad(TList<BbPatientdrug> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientdrugProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientdrugDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientdrugDataSource class.
	/// </summary>
	public class BbPatientdrugDataSourceDesigner : ProviderDataSourceDesigner<BbPatientdrug, BbPatientdrugKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientdrugDataSourceDesigner class.
		/// </summary>
		public BbPatientdrugDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientdrugSelectMethod SelectMethod
		{
			get { return ((BbPatientdrugDataSource) DataSource).SelectMethod; }
			set { SetPropertyValue("SelectMethod", value); }
		}

		/// <summary>Gets the designer action list collection for this designer.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.Design.DesignerActionListCollection"/>
		/// associated with this designer.</returns>
		public override DesignerActionListCollection ActionLists
		{
			get
			{
				DesignerActionListCollection actions = new DesignerActionListCollection();
				actions.Add(new BbPatientdrugDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientdrugDataSourceActionList

	/// <summary>
	/// Supports the BbPatientdrugDataSourceDesigner class.
	/// </summary>
	internal class BbPatientdrugDataSourceActionList : DesignerActionList
	{
		private BbPatientdrugDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientdrugDataSourceActionList(BbPatientdrugDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientdrugSelectMethod SelectMethod
		{
			get { return _designer.SelectMethod; }
			set { _designer.SelectMethod = value; }
		}

		/// <summary>
		/// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// objects contained in the list.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem"/>
		/// array that contains the items in this list.</returns>
		public override DesignerActionItemCollection GetSortedActionItems()
		{
			DesignerActionItemCollection items = new DesignerActionItemCollection();
			items.Add(new DesignerActionPropertyItem("SelectMethod", "Select Method", "Methods"));
			return items;
		}
	}

	#endregion BbPatientdrugDataSourceActionList
	
	#endregion BbPatientdrugDataSourceDesigner
	
	#region BbPatientdrugSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientdrugDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientdrugSelectMethod
	{
		/// <summary>
		/// Represents the Get method.
		/// </summary>
		Get,
		/// <summary>
		/// Represents the GetAll method.
		/// </summary>
		GetAll,
		/// <summary>
		/// Represents the GetPaged method.
		/// </summary>
		GetPaged,
		/// <summary>
		/// Represents the Find method.
		/// </summary>
		Find,
		/// <summary>
		/// Represents the GetByFupId method.
		/// </summary>
		GetByFupId,
		/// <summary>
		/// Represents the GetByStartyearStartmonthStartdayStopyearStopmonthStopday method.
		/// </summary>
		GetByStartyearStartmonthStartdayStopyearStopmonthStopday,
		/// <summary>
		/// Represents the GetByStopyearStopmonthStopday method.
		/// </summary>
		GetByStopyearStopmonthStopday,
		/// <summary>
		/// Represents the GetByPatdrugid method.
		/// </summary>
		GetByPatdrugid,
		/// <summary>
		/// Represents the GetByCommonfrequencyid method.
		/// </summary>
		GetByCommonfrequencyid,
		/// <summary>
		/// Represents the GetByDoseunitid method.
		/// </summary>
		GetByDoseunitid,
		/// <summary>
		/// Represents the GetByDrugid method.
		/// </summary>
		GetByDrugid,
		/// <summary>
		/// Represents the GetByStopreasonid method.
		/// </summary>
		GetByStopreasonid,
		/// <summary>
		/// Represents the Patientdrug_GetByFupId_Conventional method.
		/// </summary>
		Patientdrug_GetByFupId_Conventional,
		/// <summary>
		/// Represents the Patientdrug_GetByFupId_Current method.
		/// </summary>
		Patientdrug_GetByFupId_Current,
		/// <summary>
		/// Represents the Patientdrug_GetByFupId_SmallMolecule method.
		/// </summary>
		Patientdrug_GetByFupId_SmallMolecule,
		/// <summary>
		/// Represents the Patientdrug_GetByFupId_Biologic method.
		/// </summary>
		Patientdrug_GetByFupId_Biologic,
		/// <summary>
		/// Represents the PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent method.
		/// </summary>
		PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent,
		/// <summary>
		/// Represents the Patientdrug_GetByFupId_Concomitant method.
		/// </summary>
		Patientdrug_GetByFupId_Concomitant,
		/// <summary>
		/// Represents the Patientdrug_GetByFupId_Previous method.
		/// </summary>
		Patientdrug_GetByFupId_Previous
	}
	
	#endregion BbPatientdrugSelectMethod

	#region BbPatientdrugFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrug"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugFilter : SqlFilter<BbPatientdrugColumn>
	{
	}
	
	#endregion BbPatientdrugFilter

	#region BbPatientdrugExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrug"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugExpressionBuilder : SqlExpressionBuilder<BbPatientdrugColumn>
	{
	}
	
	#endregion BbPatientdrugExpressionBuilder	

	#region BbPatientdrugProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientdrugChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrug"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugProperty : ChildEntityProperty<BbPatientdrugChildEntityTypes>
	{
	}
	
	#endregion BbPatientdrugProperty
}

