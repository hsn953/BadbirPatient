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
	/// Represents the DataRepository.BbPatientCohortHistoryProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientCohortHistoryDataSourceDesigner))]
	public class BbPatientCohortHistoryDataSource : ProviderDataSource<BbPatientCohortHistory, BbPatientCohortHistoryKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryDataSource class.
		/// </summary>
		public BbPatientCohortHistoryDataSource() : base(new BbPatientCohortHistoryService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientCohortHistoryDataSourceView used by the BbPatientCohortHistoryDataSource.
		/// </summary>
		protected BbPatientCohortHistoryDataSourceView BbPatientCohortHistoryView
		{
			get { return ( View as BbPatientCohortHistoryDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientCohortHistoryDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientCohortHistorySelectMethod SelectMethod
		{
			get
			{
				BbPatientCohortHistorySelectMethod selectMethod = BbPatientCohortHistorySelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientCohortHistorySelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientCohortHistoryDataSourceView class that is to be
		/// used by the BbPatientCohortHistoryDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientCohortHistoryDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientCohortHistory, BbPatientCohortHistoryKey> GetNewDataSourceView()
		{
			return new BbPatientCohortHistoryDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPatientCohortHistoryDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientCohortHistoryDataSourceView : ProviderDataSourceView<BbPatientCohortHistory, BbPatientCohortHistoryKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientCohortHistoryDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientCohortHistoryDataSourceView(BbPatientCohortHistoryDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientCohortHistoryDataSource BbPatientCohortHistoryOwner
		{
			get { return Owner as BbPatientCohortHistoryDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientCohortHistorySelectMethod SelectMethod
		{
			get { return BbPatientCohortHistoryOwner.SelectMethod; }
			set { BbPatientCohortHistoryOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientCohortHistoryService BbPatientCohortHistoryProvider
		{
			get { return Provider as BbPatientCohortHistoryService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientCohortHistory> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientCohortHistory> results = null;
			BbPatientCohortHistory item;
			count = 0;
			
			int _patientid;
			System.Int32? _regcentreid_nullable;
			System.Int32? _studyno_nullable;
			int _chid;
			int _cohortid;
			System.Int32? sp172_Patientid;

			switch ( SelectMethod )
			{
				case BbPatientCohortHistorySelectMethod.Get:
					BbPatientCohortHistoryKey entityKey  = new BbPatientCohortHistoryKey();
					entityKey.Load(values);
					item = BbPatientCohortHistoryProvider.Get(entityKey);
					results = new TList<BbPatientCohortHistory>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientCohortHistorySelectMethod.GetAll:
                    results = BbPatientCohortHistoryProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientCohortHistorySelectMethod.GetPaged:
					results = BbPatientCohortHistoryProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientCohortHistorySelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientCohortHistoryProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientCohortHistoryProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientCohortHistorySelectMethod.GetByChid:
					_chid = ( values["Chid"] != null ) ? (int) EntityUtil.ChangeType(values["Chid"], typeof(int)) : (int)0;
					item = BbPatientCohortHistoryProvider.GetByChid(_chid);
					results = new TList<BbPatientCohortHistory>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbPatientCohortHistorySelectMethod.GetByPatientid:
					_patientid = ( values["Patientid"] != null ) ? (int) EntityUtil.ChangeType(values["Patientid"], typeof(int)) : (int)0;
					results = BbPatientCohortHistoryProvider.GetByPatientid(_patientid, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientCohortHistorySelectMethod.GetByRegcentreid:
					_regcentreid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Regcentreid"], typeof(System.Int32?));
					results = BbPatientCohortHistoryProvider.GetByRegcentreid(_regcentreid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbPatientCohortHistorySelectMethod.GetByStudyno:
					_studyno_nullable = (System.Int32?) EntityUtil.ChangeType(values["Studyno"], typeof(System.Int32?));
					results = BbPatientCohortHistoryProvider.GetByStudyno(_studyno_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// FK
				case BbPatientCohortHistorySelectMethod.GetByCohortid:
					_cohortid = ( values["Cohortid"] != null ) ? (int) EntityUtil.ChangeType(values["Cohortid"], typeof(int)) : (int)0;
					results = BbPatientCohortHistoryProvider.GetByCohortid(_cohortid, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				// Custom
				case BbPatientCohortHistorySelectMethod.PatientCohortHistory_GetMostRecentbyPatientId:
					sp172_Patientid = (System.Int32?) EntityUtil.ChangeType(values["Patientid"], typeof(System.Int32?));
					results = BbPatientCohortHistoryProvider.PatientCohortHistory_GetMostRecentbyPatientId(sp172_Patientid, StartIndex, PageSize);
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
			if ( SelectMethod == BbPatientCohortHistorySelectMethod.Get || SelectMethod == BbPatientCohortHistorySelectMethod.GetByChid )
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
				BbPatientCohortHistory entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientCohortHistoryProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPatientCohortHistory> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientCohortHistoryProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientCohortHistoryDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientCohortHistoryDataSource class.
	/// </summary>
	public class BbPatientCohortHistoryDataSourceDesigner : ProviderDataSourceDesigner<BbPatientCohortHistory, BbPatientCohortHistoryKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryDataSourceDesigner class.
		/// </summary>
		public BbPatientCohortHistoryDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientCohortHistorySelectMethod SelectMethod
		{
			get { return ((BbPatientCohortHistoryDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPatientCohortHistoryDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientCohortHistoryDataSourceActionList

	/// <summary>
	/// Supports the BbPatientCohortHistoryDataSourceDesigner class.
	/// </summary>
	internal class BbPatientCohortHistoryDataSourceActionList : DesignerActionList
	{
		private BbPatientCohortHistoryDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortHistoryDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientCohortHistoryDataSourceActionList(BbPatientCohortHistoryDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientCohortHistorySelectMethod SelectMethod
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

	#endregion BbPatientCohortHistoryDataSourceActionList
	
	#endregion BbPatientCohortHistoryDataSourceDesigner
	
	#region BbPatientCohortHistorySelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientCohortHistoryDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientCohortHistorySelectMethod
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
		/// Represents the GetByPatientid method.
		/// </summary>
		GetByPatientid,
		/// <summary>
		/// Represents the GetByRegcentreid method.
		/// </summary>
		GetByRegcentreid,
		/// <summary>
		/// Represents the GetByStudyno method.
		/// </summary>
		GetByStudyno,
		/// <summary>
		/// Represents the GetByChid method.
		/// </summary>
		GetByChid,
		/// <summary>
		/// Represents the GetByCohortid method.
		/// </summary>
		GetByCohortid,
		/// <summary>
		/// Represents the PatientCohortHistory_GetMostRecentbyPatientId method.
		/// </summary>
		PatientCohortHistory_GetMostRecentbyPatientId
	}
	
	#endregion BbPatientCohortHistorySelectMethod

	#region BbPatientCohortHistoryFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortHistoryFilter : SqlFilter<BbPatientCohortHistoryColumn>
	{
	}
	
	#endregion BbPatientCohortHistoryFilter

	#region BbPatientCohortHistoryExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortHistoryExpressionBuilder : SqlExpressionBuilder<BbPatientCohortHistoryColumn>
	{
	}
	
	#endregion BbPatientCohortHistoryExpressionBuilder	

	#region BbPatientCohortHistoryProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientCohortHistoryChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortHistory"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortHistoryProperty : ChildEntityProperty<BbPatientCohortHistoryChildEntityTypes>
	{
	}
	
	#endregion BbPatientCohortHistoryProperty
}

