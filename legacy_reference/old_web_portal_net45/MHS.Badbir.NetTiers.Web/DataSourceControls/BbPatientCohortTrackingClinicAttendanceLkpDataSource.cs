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
	/// Represents the DataRepository.BbPatientCohortTrackingClinicAttendanceLkpProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner))]
	public class BbPatientCohortTrackingClinicAttendanceLkpDataSource : ProviderDataSource<BbPatientCohortTrackingClinicAttendanceLkp, BbPatientCohortTrackingClinicAttendanceLkpKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpDataSource class.
		/// </summary>
		public BbPatientCohortTrackingClinicAttendanceLkpDataSource() : base(new BbPatientCohortTrackingClinicAttendanceLkpService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPatientCohortTrackingClinicAttendanceLkpDataSourceView used by the BbPatientCohortTrackingClinicAttendanceLkpDataSource.
		/// </summary>
		protected BbPatientCohortTrackingClinicAttendanceLkpDataSourceView BbPatientCohortTrackingClinicAttendanceLkpView
		{
			get { return ( View as BbPatientCohortTrackingClinicAttendanceLkpDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPatientCohortTrackingClinicAttendanceLkpDataSource control invokes to retrieve data.
		/// </summary>
		public BbPatientCohortTrackingClinicAttendanceLkpSelectMethod SelectMethod
		{
			get
			{
				BbPatientCohortTrackingClinicAttendanceLkpSelectMethod selectMethod = BbPatientCohortTrackingClinicAttendanceLkpSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPatientCohortTrackingClinicAttendanceLkpSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPatientCohortTrackingClinicAttendanceLkpDataSourceView class that is to be
		/// used by the BbPatientCohortTrackingClinicAttendanceLkpDataSource.
		/// </summary>
		/// <returns>An instance of the BbPatientCohortTrackingClinicAttendanceLkpDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPatientCohortTrackingClinicAttendanceLkp, BbPatientCohortTrackingClinicAttendanceLkpKey> GetNewDataSourceView()
		{
			return new BbPatientCohortTrackingClinicAttendanceLkpDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPatientCohortTrackingClinicAttendanceLkpDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPatientCohortTrackingClinicAttendanceLkpDataSourceView : ProviderDataSourceView<BbPatientCohortTrackingClinicAttendanceLkp, BbPatientCohortTrackingClinicAttendanceLkpKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPatientCohortTrackingClinicAttendanceLkpDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPatientCohortTrackingClinicAttendanceLkpDataSourceView(BbPatientCohortTrackingClinicAttendanceLkpDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPatientCohortTrackingClinicAttendanceLkpDataSource BbPatientCohortTrackingClinicAttendanceLkpOwner
		{
			get { return Owner as BbPatientCohortTrackingClinicAttendanceLkpDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPatientCohortTrackingClinicAttendanceLkpSelectMethod SelectMethod
		{
			get { return BbPatientCohortTrackingClinicAttendanceLkpOwner.SelectMethod; }
			set { BbPatientCohortTrackingClinicAttendanceLkpOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPatientCohortTrackingClinicAttendanceLkpService BbPatientCohortTrackingClinicAttendanceLkpProvider
		{
			get { return Provider as BbPatientCohortTrackingClinicAttendanceLkpService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPatientCohortTrackingClinicAttendanceLkp> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPatientCohortTrackingClinicAttendanceLkp> results = null;
			BbPatientCohortTrackingClinicAttendanceLkp item;
			count = 0;
			
			int _clinicAttendanceId;

			switch ( SelectMethod )
			{
				case BbPatientCohortTrackingClinicAttendanceLkpSelectMethod.Get:
					BbPatientCohortTrackingClinicAttendanceLkpKey entityKey  = new BbPatientCohortTrackingClinicAttendanceLkpKey();
					entityKey.Load(values);
					item = BbPatientCohortTrackingClinicAttendanceLkpProvider.Get(entityKey);
					results = new TList<BbPatientCohortTrackingClinicAttendanceLkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPatientCohortTrackingClinicAttendanceLkpSelectMethod.GetAll:
                    results = BbPatientCohortTrackingClinicAttendanceLkpProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPatientCohortTrackingClinicAttendanceLkpSelectMethod.GetPaged:
					results = BbPatientCohortTrackingClinicAttendanceLkpProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPatientCohortTrackingClinicAttendanceLkpSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPatientCohortTrackingClinicAttendanceLkpProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPatientCohortTrackingClinicAttendanceLkpProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPatientCohortTrackingClinicAttendanceLkpSelectMethod.GetByClinicAttendanceId:
					_clinicAttendanceId = ( values["ClinicAttendanceId"] != null ) ? (int) EntityUtil.ChangeType(values["ClinicAttendanceId"], typeof(int)) : (int)0;
					item = BbPatientCohortTrackingClinicAttendanceLkpProvider.GetByClinicAttendanceId(_clinicAttendanceId);
					results = new TList<BbPatientCohortTrackingClinicAttendanceLkp>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				// M:M
				// Custom
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
			if ( SelectMethod == BbPatientCohortTrackingClinicAttendanceLkpSelectMethod.Get || SelectMethod == BbPatientCohortTrackingClinicAttendanceLkpSelectMethod.GetByClinicAttendanceId )
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
				BbPatientCohortTrackingClinicAttendanceLkp entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPatientCohortTrackingClinicAttendanceLkpProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPatientCohortTrackingClinicAttendanceLkp> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPatientCohortTrackingClinicAttendanceLkpProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPatientCohortTrackingClinicAttendanceLkpDataSource class.
	/// </summary>
	public class BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner : ProviderDataSourceDesigner<BbPatientCohortTrackingClinicAttendanceLkp, BbPatientCohortTrackingClinicAttendanceLkpKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner class.
		/// </summary>
		public BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientCohortTrackingClinicAttendanceLkpSelectMethod SelectMethod
		{
			get { return ((BbPatientCohortTrackingClinicAttendanceLkpDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPatientCohortTrackingClinicAttendanceLkpDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPatientCohortTrackingClinicAttendanceLkpDataSourceActionList

	/// <summary>
	/// Supports the BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner class.
	/// </summary>
	internal class BbPatientCohortTrackingClinicAttendanceLkpDataSourceActionList : DesignerActionList
	{
		private BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPatientCohortTrackingClinicAttendanceLkpDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPatientCohortTrackingClinicAttendanceLkpDataSourceActionList(BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPatientCohortTrackingClinicAttendanceLkpSelectMethod SelectMethod
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

	#endregion BbPatientCohortTrackingClinicAttendanceLkpDataSourceActionList
	
	#endregion BbPatientCohortTrackingClinicAttendanceLkpDataSourceDesigner
	
	#region BbPatientCohortTrackingClinicAttendanceLkpSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPatientCohortTrackingClinicAttendanceLkpDataSource.SelectMethod property.
	/// </summary>
	public enum BbPatientCohortTrackingClinicAttendanceLkpSelectMethod
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
		/// Represents the GetByClinicAttendanceId method.
		/// </summary>
		GetByClinicAttendanceId
	}
	
	#endregion BbPatientCohortTrackingClinicAttendanceLkpSelectMethod

	#region BbPatientCohortTrackingClinicAttendanceLkpFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingClinicAttendanceLkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingClinicAttendanceLkpFilter : SqlFilter<BbPatientCohortTrackingClinicAttendanceLkpColumn>
	{
	}
	
	#endregion BbPatientCohortTrackingClinicAttendanceLkpFilter

	#region BbPatientCohortTrackingClinicAttendanceLkpExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingClinicAttendanceLkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingClinicAttendanceLkpExpressionBuilder : SqlExpressionBuilder<BbPatientCohortTrackingClinicAttendanceLkpColumn>
	{
	}
	
	#endregion BbPatientCohortTrackingClinicAttendanceLkpExpressionBuilder	

	#region BbPatientCohortTrackingClinicAttendanceLkpProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPatientCohortTrackingClinicAttendanceLkpChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientCohortTrackingClinicAttendanceLkp"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientCohortTrackingClinicAttendanceLkpProperty : ChildEntityProperty<BbPatientCohortTrackingClinicAttendanceLkpChildEntityTypes>
	{
	}
	
	#endregion BbPatientCohortTrackingClinicAttendanceLkpProperty
}

