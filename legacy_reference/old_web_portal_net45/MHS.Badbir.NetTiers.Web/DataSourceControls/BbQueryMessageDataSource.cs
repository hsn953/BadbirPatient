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
	/// Represents the DataRepository.BbQueryMessageProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbQueryMessageDataSourceDesigner))]
	public class BbQueryMessageDataSource : ProviderDataSource<BbQueryMessage, BbQueryMessageKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageDataSource class.
		/// </summary>
		public BbQueryMessageDataSource() : base(new BbQueryMessageService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbQueryMessageDataSourceView used by the BbQueryMessageDataSource.
		/// </summary>
		protected BbQueryMessageDataSourceView BbQueryMessageView
		{
			get { return ( View as BbQueryMessageDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbQueryMessageDataSource control invokes to retrieve data.
		/// </summary>
		public BbQueryMessageSelectMethod SelectMethod
		{
			get
			{
				BbQueryMessageSelectMethod selectMethod = BbQueryMessageSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbQueryMessageSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbQueryMessageDataSourceView class that is to be
		/// used by the BbQueryMessageDataSource.
		/// </summary>
		/// <returns>An instance of the BbQueryMessageDataSourceView class.</returns>
		protected override BaseDataSourceView<BbQueryMessage, BbQueryMessageKey> GetNewDataSourceView()
		{
			return new BbQueryMessageDataSourceView(this, DefaultViewName);
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
	/// Supports the BbQueryMessageDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbQueryMessageDataSourceView : ProviderDataSourceView<BbQueryMessage, BbQueryMessageKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbQueryMessageDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbQueryMessageDataSourceView(BbQueryMessageDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbQueryMessageDataSource BbQueryMessageOwner
		{
			get { return Owner as BbQueryMessageDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbQueryMessageSelectMethod SelectMethod
		{
			get { return BbQueryMessageOwner.SelectMethod; }
			set { BbQueryMessageOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbQueryMessageService BbQueryMessageProvider
		{
			get { return Provider as BbQueryMessageService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbQueryMessage> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbQueryMessage> results = null;
			BbQueryMessage item;
			count = 0;
			
			int _qid;
			int _qmId;

			switch ( SelectMethod )
			{
				case BbQueryMessageSelectMethod.Get:
					BbQueryMessageKey entityKey  = new BbQueryMessageKey();
					entityKey.Load(values);
					item = BbQueryMessageProvider.Get(entityKey);
					results = new TList<BbQueryMessage>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbQueryMessageSelectMethod.GetAll:
                    results = BbQueryMessageProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbQueryMessageSelectMethod.GetPaged:
					results = BbQueryMessageProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbQueryMessageSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbQueryMessageProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbQueryMessageProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbQueryMessageSelectMethod.GetByQmId:
					_qmId = ( values["QmId"] != null ) ? (int) EntityUtil.ChangeType(values["QmId"], typeof(int)) : (int)0;
					item = BbQueryMessageProvider.GetByQmId(_qmId);
					results = new TList<BbQueryMessage>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbQueryMessageSelectMethod.GetByQid:
					_qid = ( values["Qid"] != null ) ? (int) EntityUtil.ChangeType(values["Qid"], typeof(int)) : (int)0;
					results = BbQueryMessageProvider.GetByQid(_qid, this.StartIndex, this.PageSize, out count);
					break;
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
			if ( SelectMethod == BbQueryMessageSelectMethod.Get || SelectMethod == BbQueryMessageSelectMethod.GetByQmId )
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
				BbQueryMessage entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbQueryMessageProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbQueryMessage> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbQueryMessageProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbQueryMessageDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbQueryMessageDataSource class.
	/// </summary>
	public class BbQueryMessageDataSourceDesigner : ProviderDataSourceDesigner<BbQueryMessage, BbQueryMessageKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbQueryMessageDataSourceDesigner class.
		/// </summary>
		public BbQueryMessageDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryMessageSelectMethod SelectMethod
		{
			get { return ((BbQueryMessageDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbQueryMessageDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbQueryMessageDataSourceActionList

	/// <summary>
	/// Supports the BbQueryMessageDataSourceDesigner class.
	/// </summary>
	internal class BbQueryMessageDataSourceActionList : DesignerActionList
	{
		private BbQueryMessageDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbQueryMessageDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbQueryMessageDataSourceActionList(BbQueryMessageDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbQueryMessageSelectMethod SelectMethod
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

	#endregion BbQueryMessageDataSourceActionList
	
	#endregion BbQueryMessageDataSourceDesigner
	
	#region BbQueryMessageSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbQueryMessageDataSource.SelectMethod property.
	/// </summary>
	public enum BbQueryMessageSelectMethod
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
		/// Represents the GetByQid method.
		/// </summary>
		GetByQid,
		/// <summary>
		/// Represents the GetByQmId method.
		/// </summary>
		GetByQmId
	}
	
	#endregion BbQueryMessageSelectMethod

	#region BbQueryMessageFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryMessageFilter : SqlFilter<BbQueryMessageColumn>
	{
	}
	
	#endregion BbQueryMessageFilter

	#region BbQueryMessageExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryMessageExpressionBuilder : SqlExpressionBuilder<BbQueryMessageColumn>
	{
	}
	
	#endregion BbQueryMessageExpressionBuilder	

	#region BbQueryMessageProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbQueryMessageChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbQueryMessage"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbQueryMessageProperty : ChildEntityProperty<BbQueryMessageChildEntityTypes>
	{
	}
	
	#endregion BbQueryMessageProperty
}

