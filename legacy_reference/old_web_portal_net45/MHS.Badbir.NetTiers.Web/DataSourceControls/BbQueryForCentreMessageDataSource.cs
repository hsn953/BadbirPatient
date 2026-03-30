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
	/// Represents the DataRepository.BbQueryForCentreMessageProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbQueryForCentreMessageDataSourceDesigner))]
	public class BbQueryForCentreMessageDataSource : ProviderDataSource<BbQueryForCentreMessage, BbQueryForCentreMessageKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageDataSource class.
		/// </summary>
		public BbQueryForCentreMessageDataSource() : base(new BbQueryForCentreMessageService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbQueryForCentreMessageDataSourceView used by the BbQueryForCentreMessageDataSource.
		/// </summary>
		protected BbQueryForCentreMessageDataSourceView BbQueryForCentreMessageView
		{
			get { return ( View as BbQueryForCentreMessageDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbQueryForCentreMessageDataSource control invokes to retrieve data.
		/// </summary>
		public BbQueryForCentreMessageSelectMethod SelectMethod
		{
			get
			{
				BbQueryForCentreMessageSelectMethod selectMethod = BbQueryForCentreMessageSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbQueryForCentreMessageSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbQueryForCentreMessageDataSourceView class that is to be
		/// used by the BbQueryForCentreMessageDataSource.
		/// </summary>
		/// <returns>An instance of the BbQueryForCentreMessageDataSourceView class.</returns>
		protected override BaseDataSourceView<BbQueryForCentreMessage, BbQueryForCentreMessageKey> GetNewDataSourceView()
		{
			return new BbQueryForCentreMessageDataSourceView(this, DefaultViewName);
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
	/// Supports the BbQueryForCentreMessageDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbQueryForCentreMessageDataSourceView : ProviderDataSourceView<BbQueryForCentreMessage, BbQueryForCentreMessageKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbQueryForCentreMessageDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbQueryForCentreMessageDataSourceView(BbQueryForCentreMessageDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbQueryForCentreMessageDataSource BbQueryForCentreMessageOwner
		{
			get { return Owner as BbQueryForCentreMessageDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbQueryForCentreMessageSelectMethod SelectMethod
		{
			get { return BbQueryForCentreMessageOwner.SelectMethod; }
			set { BbQueryForCentreMessageOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbQueryForCentreMessageService BbQueryForCentreMessageProvider
		{
			get { return Provider as BbQueryForCentreMessageService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbQueryForCentreMessage> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbQueryForCentreMessage> results = null;
			BbQueryForCentreMessage item;
			count = 0;
			
			int _qmId;
			int _qid;

			switch ( SelectMethod )
			{
				case BbQueryForCentreMessageSelectMethod.Get:
					BbQueryForCentreMessageKey entityKey  = new BbQueryForCentreMessageKey();
					entityKey.Load(values);
					item = BbQueryForCentreMessageProvider.Get(entityKey);
					results = new TList<BbQueryForCentreMessage>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbQueryForCentreMessageSelectMethod.GetAll:
                    results = BbQueryForCentreMessageProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbQueryForCentreMessageSelectMethod.GetPaged:
					results = BbQueryForCentreMessageProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbQueryForCentreMessageSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbQueryForCentreMessageProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbQueryForCentreMessageProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbQueryForCentreMessageSelectMethod.GetByQmId:
					_qmId = ( values["QmId"] != null ) ? (int) EntityUtil.ChangeType(values["QmId"], typeof(int)) : (int)0;
					item = BbQueryForCentreMessageProvider.GetByQmId(_qmId);
					results = new TList<BbQueryForCentreMessage>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				// FK
				case BbQueryForCentreMessageSelectMethod.GetByQid:
					_qid = ( values["Qid"] != null ) ? (int) EntityUtil.ChangeType(values["Qid"], typeof(int)) : (int)0;
					results = BbQueryForCentreMessageProvider.GetByQid(_qid, this.StartIndex, this.PageSize, out count);
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
			if ( SelectMethod == BbQueryForCentreMessageSelectMethod.Get || SelectMethod == BbQueryForCentreMessageSelectMethod.GetByQmId )
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
				BbQueryForCentreMessage entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbQueryForCentreMessageProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbQueryForCentreMessage> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbQueryForCentreMessageProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbQueryForCentreMessageDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbQueryForCentreMessageDataSource class.
	/// </summary>
	public class BbQueryForCentreMessageDataSourceDesigner : ProviderDataSourceDesigner<BbQueryForCentreMessage, BbQueryForCentreMessageKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageDataSourceDesigner class.
		/// </summary>
		public BbQueryForCentreMessageDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryForCentreMessageSelectMethod SelectMethod
		{
			get { return ((BbQueryForCentreMessageDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbQueryForCentreMessageDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbQueryForCentreMessageDataSourceActionList

	/// <summary>
	/// Supports the BbQueryForCentreMessageDataSourceDesigner class.
	/// </summary>
	internal class BbQueryForCentreMessageDataSourceActionList : DesignerActionList
	{
		private BbQueryForCentreMessageDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbQueryForCentreMessageDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbQueryForCentreMessageDataSourceActionList(BbQueryForCentreMessageDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryForCentreMessageSelectMethod SelectMethod
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

	#endregion BbQueryForCentreMessageDataSourceActionList
	
	#endregion BbQueryForCentreMessageDataSourceDesigner
	
	#region BbQueryForCentreMessageSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbQueryForCentreMessageDataSource.SelectMethod property.
	/// </summary>
	public enum BbQueryForCentreMessageSelectMethod
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
		/// Represents the GetByQmId method.
		/// </summary>
		GetByQmId,
		/// <summary>
		/// Represents the GetByQid method.
		/// </summary>
		GetByQid
	}
	
	#endregion BbQueryForCentreMessageSelectMethod

	#region BbQueryForCentreMessageFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentreMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreMessageFilter : SqlFilter<BbQueryForCentreMessageColumn>
	{
	}
	
	#endregion BbQueryForCentreMessageFilter

	#region BbQueryForCentreMessageExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentreMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreMessageExpressionBuilder : SqlExpressionBuilder<BbQueryForCentreMessageColumn>
	{
	}
	
	#endregion BbQueryForCentreMessageExpressionBuilder	

	#region BbQueryForCentreMessageProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbQueryForCentreMessageChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryForCentreMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryForCentreMessageProperty : ChildEntityProperty<BbQueryForCentreMessageChildEntityTypes>
	{
	}
	
	#endregion BbQueryForCentreMessageProperty
}

