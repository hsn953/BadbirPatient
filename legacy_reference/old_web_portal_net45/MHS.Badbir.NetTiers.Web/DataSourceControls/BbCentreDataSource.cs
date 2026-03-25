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
	/// Represents the DataRepository.BbCentreProvider object that provides
	/// data to data-bound controls in multi-tier Web application architectures.
	/// </summary>
	[Designer(typeof(BbCentreDataSourceDesigner))]
	public class BbCentreDataSource : ProviderDataSource<BbCentre, BbCentreKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentreDataSource class.
		/// </summary>
		public BbCentreDataSource() : base(new BbCentreService())
		{
		}

		#endregion Constructors
		
		#region Properties
		
		/// <summary>
		/// Gets a reference to the BbCentreDataSourceView used by the BbCentreDataSource.
		/// </summary>
		protected BbCentreDataSourceView BbCentreView
		{
			get { return ( View as BbCentreDataSourceView ); }
		}
		
		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the BbCentreDataSource control invokes to retrieve data.
		/// </summary>
		public BbCentreSelectMethod SelectMethod
		{
			get
			{
				BbCentreSelectMethod selectMethod = BbCentreSelectMethod.GetAll;
				Object method = ViewState["SelectMethod"];
				if ( method != null )
				{
					selectMethod = (BbCentreSelectMethod) method;
				}
				return selectMethod;
			}
			set { ViewState["SelectMethod"] = value; }
		}

		#endregion Properties
		
		#region Methods

		/// <summary>
		/// Creates a new instance of the BbCentreDataSourceView class that is to be
		/// used by the BbCentreDataSource.
		/// </summary>
		/// <returns>An instance of the BbCentreDataSourceView class.</returns>
		protected override BaseDataSourceView<BbCentre, BbCentreKey> GetNewDataSourceView()
		{
			return new BbCentreDataSourceView(this, DefaultViewName);
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
	/// Supports the BbCentreDataSource control and provides an interface for
	/// data-bound controls to perform data operations with business and data objects.
	/// </summary>
	public class BbCentreDataSourceView : ProviderDataSourceView<BbCentre, BbCentreKey>
	{
		#region Declarations

		#endregion Declarations
		
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbCentreDataSourceView class.
		/// </summary>
		/// <param name="owner">A reference to the BbCentreDataSource which created this instance.</param>
		/// <param name="viewName">The name of the view.</param>
		public BbCentreDataSourceView(BbCentreDataSource owner, String viewName)
			: base(owner, viewName)
		{
		}
		
		#endregion Constructors
		
		#region Properties

		/// <summary>
		/// Gets a strongly-typed reference to the Owner property.
		/// </summary>
		internal BbCentreDataSource BbCentreOwner
		{
			get { return Owner as BbCentreDataSource; }
		}

		/// <summary>
		/// Gets or sets the name of the method or function that
		/// the DataSource control invokes to retrieve data.
		/// </summary>
		internal BbCentreSelectMethod SelectMethod
		{
			get { return BbCentreOwner.SelectMethod; }
			set { BbCentreOwner.SelectMethod = value; }
		}

		/// <summary>
		/// Gets a strongly typed reference to the Provider property.
		/// </summary>
		internal BbCentreService BbCentreProvider
		{
			get { return Provider as BbCentreService; }
		}

		#endregion Properties
		
		#region Methods
		 
		/// <summary>
		/// Gets a collection of Entity objects based on the value of the SelectMethod property.
		/// </summary>
		/// <param name="count">The total number of rows in the DataSource.</param>
	    /// <param name="values"></param>
		/// <returns>A collection of Entity objects.</returns>
		protected override IList<BbCentre> GetSelectData(IDictionary values, out int count)
		{
            if (values == null || values.Count == 0) values = CollectionsUtil.CreateCaseInsensitiveHashtable(GetParameterValues());
            
			Hashtable customOutput = CollectionsUtil.CreateCaseInsensitiveHashtable();
			IList<BbCentre> results = null;
			BbCentre item;
			count = 0;
			
			System.Int32 _centreid;
			System.String _altCentreId_nullable;
			System.Int32? _centreregionid_nullable;
			System.Int32? _centrestatusid_nullable;
			System.Int32? _ukcrNregionid_nullable;
			System.Guid _userid;
			System.Int32 _externalStudyId;
			System.Int32? sp103_BadbirUserid;
			System.Int32? sp103_IsTraining;
			System.Int32? sp104_BadbirUserid;
			System.Int32? sp104_IsTraining;

			switch ( SelectMethod )
			{
				case BbCentreSelectMethod.Get:
					BbCentreKey entityKey  = new BbCentreKey();
					entityKey.Load(values);
					item = BbCentreProvider.Get(entityKey);
					results = new TList<BbCentre>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				case BbCentreSelectMethod.GetAll:
                    results = BbCentreProvider.GetAll(StartIndex, PageSize, out count);
                    break;
				case BbCentreSelectMethod.GetPaged:
					results = BbCentreProvider.GetPaged(WhereClause, OrderBy, PageIndex, PageSize, out count);
					break;
				case BbCentreSelectMethod.Find:
					if ( FilterParameters != null )
						results = BbCentreProvider.Find(FilterParameters, OrderBy, StartIndex, PageSize, out count);
					else
						results = BbCentreProvider.Find(WhereClause, StartIndex, PageSize, out count);
                    break;
				// PK
				case BbCentreSelectMethod.GetByCentreid:
					_centreid = ( values["Centreid"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["Centreid"], typeof(System.Int32)) : (int)0;
					item = BbCentreProvider.GetByCentreid(_centreid);
					results = new TList<BbCentre>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// IX
				case BbCentreSelectMethod.GetByAltCentreId:
					_altCentreId_nullable = (System.String) EntityUtil.ChangeType(values["AltCentreId"], typeof(System.String));
					item = BbCentreProvider.GetByAltCentreId(_altCentreId_nullable);
					results = new TList<BbCentre>();
					if ( item != null ) results.Add(item);
					count = results.Count;
					break;
				// FK
				case BbCentreSelectMethod.GetByCentreregionid:
					_centreregionid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Centreregionid"], typeof(System.Int32?));
					results = BbCentreProvider.GetByCentreregionid(_centreregionid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbCentreSelectMethod.GetByCentrestatusid:
					_centrestatusid_nullable = (System.Int32?) EntityUtil.ChangeType(values["Centrestatusid"], typeof(System.Int32?));
					results = BbCentreProvider.GetByCentrestatusid(_centrestatusid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				case BbCentreSelectMethod.GetByUkcrNregionid:
					_ukcrNregionid_nullable = (System.Int32?) EntityUtil.ChangeType(values["UkcrNregionid"], typeof(System.Int32?));
					results = BbCentreProvider.GetByUkcrNregionid(_ukcrNregionid_nullable, this.StartIndex, this.PageSize, out count);
					break;
				// M:M
				case BbCentreSelectMethod.GetByUseridFromBbAdditionalUserAndCentre:
					_userid = ( values["Userid"] != null ) ? (System.Guid) EntityUtil.ChangeType(values["Userid"], typeof(System.Guid)) : Guid.Empty;
					results = BbCentreProvider.GetByUseridFromBbAdditionalUserAndCentre(_userid, this.StartIndex, this.PageSize, out count);
					break;
				case BbCentreSelectMethod.GetByExternalStudyIdFromBbCentreExternalStudyLink:
					_externalStudyId = ( values["ExternalStudyId"] != null ) ? (System.Int32) EntityUtil.ChangeType(values["ExternalStudyId"], typeof(System.Int32)) : (int)0;
					results = BbCentreProvider.GetByExternalStudyIdFromBbCentreExternalStudyLink(_externalStudyId, this.StartIndex, this.PageSize, out count);
					break;
				// Custom
				case BbCentreSelectMethod.Centre_GetByBADBIRUserid:
					sp103_BadbirUserid = (System.Int32?) EntityUtil.ChangeType(values["BadbirUserid"], typeof(System.Int32?));
					sp103_IsTraining = (System.Int32?) EntityUtil.ChangeType(values["IsTraining"], typeof(System.Int32?));
					results = BbCentreProvider.Centre_GetByBADBIRUserid(sp103_BadbirUserid, sp103_IsTraining, StartIndex, PageSize);
					break;
				case BbCentreSelectMethod.Centre_GetByBADBIRUseridAndTraining:
					sp104_BadbirUserid = (System.Int32?) EntityUtil.ChangeType(values["BadbirUserid"], typeof(System.Int32?));
					sp104_IsTraining = (System.Int32?) EntityUtil.ChangeType(values["IsTraining"], typeof(System.Int32?));
					results = BbCentreProvider.Centre_GetByBADBIRUseridAndTraining(sp104_BadbirUserid, sp104_IsTraining, StartIndex, PageSize);
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
			if ( SelectMethod == BbCentreSelectMethod.Get || SelectMethod == BbCentreSelectMethod.GetByCentreid )
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
				BbCentre entity = GetCurrentEntity();
				
				if ( entity != null )
				{
					// init transaction manager
					GetTransactionManager();
					// execute deep load method
					BbCentreProvider.DeepLoad(GetCurrentEntity(), EnableRecursiveDeepLoad);
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
		internal override void DeepLoad(TList<BbCentre> entityList, ProviderDataSourceDeepLoadList properties)
		{
			// init transaction manager
			GetTransactionManager();
			// execute deep load method
			BbCentreProvider.DeepLoad(entityList, properties.Recursive, properties.Method, properties.GetTypes());
		}

		#endregion Select Methods
	}
	
	#region BbCentreDataSourceDesigner

	/// <summary>
	/// Provides design-time support in a design host for the BbCentreDataSource class.
	/// </summary>
	public class BbCentreDataSourceDesigner : ProviderDataSourceDesigner<BbCentre, BbCentreKey>
	{
		/// <summary>
		/// Initializes a new instance of the BbCentreDataSourceDesigner class.
		/// </summary>
		public BbCentreDataSourceDesigner()
		{
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbCentreSelectMethod SelectMethod
		{
			get { return ((BbCentreDataSource) DataSource).SelectMethod; }
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
				actions.Add(new BbCentreDataSourceActionList(this));
				actions.AddRange(base.ActionLists);
				return actions;
			}
		}
	}

	#region BbCentreDataSourceActionList

	/// <summary>
	/// Supports the BbCentreDataSourceDesigner class.
	/// </summary>
	internal class BbCentreDataSourceActionList : DesignerActionList
	{
		private BbCentreDataSourceDesigner _designer;

		/// <summary>
		/// Initializes a new instance of the BbCentreDataSourceActionList class.
		/// </summary>
		/// <param name="designer"></param>
		public BbCentreDataSourceActionList(BbCentreDataSourceDesigner designer) : base(designer.Component)
		{
			_designer = designer;
		}

		/// <summary>
		/// Gets or sets the SelectMethod property.
		/// </summary>
		public BbCentreSelectMethod SelectMethod
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

	#endregion BbCentreDataSourceActionList
	
	#endregion BbCentreDataSourceDesigner
	
	#region BbCentreSelectMethod
	
	/// <summary>
	/// Enumeration of method names available for the BbCentreDataSource.SelectMethod property.
	/// </summary>
	public enum BbCentreSelectMethod
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
		/// Represents the GetByCentreid method.
		/// </summary>
		GetByCentreid,
		/// <summary>
		/// Represents the GetByAltCentreId method.
		/// </summary>
		GetByAltCentreId,
		/// <summary>
		/// Represents the GetByCentreregionid method.
		/// </summary>
		GetByCentreregionid,
		/// <summary>
		/// Represents the GetByCentrestatusid method.
		/// </summary>
		GetByCentrestatusid,
		/// <summary>
		/// Represents the GetByUkcrNregionid method.
		/// </summary>
		GetByUkcrNregionid,
		/// <summary>
		/// Represents the GetByUseridFromBbAdditionalUserAndCentre method.
		/// </summary>
		GetByUseridFromBbAdditionalUserAndCentre,
		/// <summary>
		/// Represents the GetByExternalStudyIdFromBbCentreExternalStudyLink method.
		/// </summary>
		GetByExternalStudyIdFromBbCentreExternalStudyLink,
		/// <summary>
		/// Represents the Centre_GetByBADBIRUserid method.
		/// </summary>
		Centre_GetByBADBIRUserid,
		/// <summary>
		/// Represents the Centre_GetByBADBIRUseridAndTraining method.
		/// </summary>
		Centre_GetByBADBIRUseridAndTraining
	}
	
	#endregion BbCentreSelectMethod

	#region BbCentreFilter
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilter&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreFilter : SqlFilter<BbCentreColumn>
	{
	}
	
	#endregion BbCentreFilter

	#region BbCentreExpressionBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlExpressionBuilder&lt;EntityColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreExpressionBuilder : SqlExpressionBuilder<BbCentreColumn>
	{
	}
	
	#endregion BbCentreExpressionBuilder	

	#region BbCentreProperty
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ChildEntityProperty&lt;BbCentreChildEntityTypes&gt;"/> class
	/// that is used exclusively with a <see cref="BbCentre"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbCentreProperty : ChildEntityProperty<BbCentreChildEntityTypes>
	{
	}
	
	#endregion BbCentreProperty
}

