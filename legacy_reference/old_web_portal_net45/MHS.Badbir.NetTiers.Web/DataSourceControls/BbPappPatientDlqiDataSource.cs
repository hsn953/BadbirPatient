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
	/// Represents the DataRepository.BbPappPatientDlqiProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbPappPatientDlqiDataSourceDesigner))]
	public class BbPappPatientDlqiDataSource : ProviderDataSource<BbPappPatientDlqi, BbPappPatientDlqiKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiDataSource class.
		/// </summary>
		public BbPappPatientDlqiDataSource() : base(new BbPappPatientDlqiService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbPappPatientDlqiDataSourceView used by the BbPappPatientDlqiDataSource.
		/// </summary>
		protected BbPappPatientDlqiDataSourceView BbPappPatientDlqiView
		{
			get { return ( View as BbPappPatientDlqiDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbPappPatientDlqiDataSource control invokes to retrieve data.
		/// </summary>
		public BbPappPatientDlqiSelectMethod SelectMethod
		{
			get
			{
				BbPappPatientDlqiSelectMethod selectMethod = BbPappPatientDlqiSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbPappPatientDlqiSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbPappPatientDlqiDataSourceView class that is to be
		/// used by the BbPappPatientDlqiDataSource.
		/// </summary>
		/// <returns>An instance of the BbPappPatientDlqiDataSourceView class.</returns>
		protected override BaseDataSourceView<BbPappPatientDlqi, BbPappPatientDlqiKey> GetNewDataSourceView()
		{
			return new BbPappPatientDlqiDataSourceView(this, DefaultViewName);
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
	/// Supports the BbPappPatientDlqiDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbPappPatientDlqiDataSourceView : ProviderDataSourceView<BbPappPatientDlqi, BbPappPatientDlqiKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbPappPatientDlqiDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbPappPatientDlqiDataSourceView(BbPappPatientDlqiDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbPappPatientDlqiDataSource BbPappPatientDlqiOwner
		{
			get { return Owner as BbPappPatientDlqiDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbPappPatientDlqiSelectMethod SelectMethod
		{
			get { return BbPappPatientDlqiOwner.SelectMethod; }
			set { BbPappPatientDlqiOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbPappPatientDlqiService BbPappPatientDlqiProvider
		{
			get { return Provider as BbPappPatientDlqiService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbPappPatientDlqi> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbPappPatientDlqi> results = null;
			BbPappPatientDlqi item;
			count = 0;
			
			int _formId;
			int _chid;

			switch ( SelectMethod )
			{
				case BbPappPatientDlqiSelectMethod.Get:
					BbPappPatientDlqiKey entityKey  = new BbPappPatientDlqiKey();
					entityKey.Load(values);
					item = BbPappPatientDlqiProvider.Get(entityKey);
					results = new TList<BbPappPatientDlqi>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbPappPatientDlqiSelectMethod.GetAll:
                    results = BbPappPatientDlqiProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbPappPatientDlqiSelectMethod.GetPaged:
					results = BbPappPatientDlqiProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbPappPatientDlqiSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbPappPatientDlqiProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbPappPatientDlqiProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbPappPatientDlqiSelectMethod.GetByFormId:
					_formId = ( values["FormId"] != null ) ? (int) EntityUtil.ChangeType(values["FormId"], typeof(int)) : (int)0;
					item = BbPappPatientDlqiProvider.GetByFormId(_formId);
					results = new TList<BbPappPatientDlqi>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbPappPatientDlqiSelectMethod.GetByChid:
					_chid = ( values["Chid"] != null ) ? (int) EntityUtil.ChangeType(values["Chid"], typeof(int)) : (int)0;
					results = BbPappPatientDlqiProvider.GetByChid(_chid, this.StartIndex, this.PageSize, out count);
					break;
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
			if ( SelectMethod == BbPappPatientDlqiSelectMethod.Get || SelectMethod == BbPappPatientDlqiSelectMethod.GetByFormId )
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
				BbPappPatientDlqi entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbPappPatientDlqiProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbPappPatientDlqi> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbPappPatientDlqiProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbPappPatientDlqiDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbPappPatientDlqiDataSource class.
	/// </summary>
	public class BbPappPatientDlqiDataSourceDesigner : ProviderDataSourceDesigner<BbPappPatientDlqi, BbPappPatientDlqiKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiDataSourceDesigner class.
		/// </summary>
		public BbPappPatientDlqiDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPappPatientDlqiSelectMethod SelectMethod
		{
			get { return ((BbPappPatientDlqiDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbPappPatientDlqiDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbPappPatientDlqiDataSourceActionList

	/// <summary>
	/// Supports the BbPappPatientDlqiDataSourceDesigner class.
	/// </summary>
	internal class BbPappPatientDlqiDataSourceActionList : DesignerActionList
	{
		private BbPappPatientDlqiDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbPappPatientDlqiDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbPappPatientDlqiDataSourceActionList(BbPappPatientDlqiDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbPappPatientDlqiSelectMethod SelectMethod
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

	#endregion BbPappPatientDlqiDataSourceActionList
	
	#endregion BbPappPatientDlqiDataSourceDesigner
	
	#region BbPappPatientDlqiSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbPappPatientDlqiDataSource.SelectMethod property.
	/// </summary>
	public enum BbPappPatientDlqiSelectMethod
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
		/// Represents the GetByFormId method.
		/// </summary>
		GetByFormId,
		/// <summary>
		/// Represents the GetByChid method.
		/// </summary>
		GetByChid
	}
	
	#endregion BbPappPatientDlqiSelectMethod

	#region BbPappPatientDlqiFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientDlqi"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientDlqiFilter : SqlFilter<BbPappPatientDlqiColumn>
	{
	}
	
	#endregion BbPappPatientDlqiFilter

	#region BbPappPatientDlqiExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientDlqi"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientDlqiExpressionBuilder : SqlExpressionBuilder<BbPappPatientDlqiColumn>
	{
	}
	
	#endregion BbPappPatientDlqiExpressionBuilder	

	#region BbPappPatientDlqiProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbPappPatientDlqiChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbPappPatientDlqi"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPappPatientDlqiProperty : ChildEntityProperty<BbPappPatientDlqiChildEntityTypes>
	{
	}
	
	#endregion BbPappPatientDlqiProperty
}

