#region Using directives

using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;

using MHS.Badbir.NetTiers.Entities;
using MHS.Badbir.NetTiers.Data;

#endregion

namespace MHS.Badbir.NetTiers.Data.Bases
{	
	///<summary>
	/// This class is the base class for any <see cref="BbPatientdrugProviderBase"/> implementation.
	/// It exposes CRUD methods as well as selecting on index, foreign keys and custom stored procedures.
	///</summary>
	public abstract partial class BbPatientdrugProviderBaseCore : EntityProviderBase<MHS.Badbir.NetTiers.Entities.BbPatientdrug, MHS.Badbir.NetTiers.Entities.BbPatientdrugKey>
	{		
		#region Get from Many To Many Relationship Functions
		#endregion	
		
		#region Delete Methods

		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to delete.</param>
		/// <returns>Returns true if operation suceeded.</returns>
		public override bool Delete(TransactionManager transactionManager, MHS.Badbir.NetTiers.Entities.BbPatientdrugKey key)
		{
			return Delete(transactionManager, key.Patdrugid);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="_patdrugid">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public bool Delete(int _patdrugid)
		{
			return Delete(null, _patdrugid);
		}
		
		/// <summary>
		/// 	Deletes a row from the DataSource.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patdrugid">. Primary Key.</param>
		/// <remarks>Deletes based on primary key(s).</remarks>
		/// <returns>Returns true if operation suceeded.</returns>
		public abstract bool Delete(TransactionManager transactionManager, int _patdrugid);		
		
		#endregion Delete Methods
		
		#region Get By Foreign Key Functions
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbCommonFrequencylkp key.
		///		FK_bbPatientdrug_bbCommonFrequencylkp Description: 
		/// </summary>
		/// <param name="_commonfrequencyid"></param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByCommonfrequencyid(System.Int32? _commonfrequencyid)
		{
			int count = -1;
			return GetByCommonfrequencyid(_commonfrequencyid, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbCommonFrequencylkp key.
		///		FK_bbPatientdrug_bbCommonFrequencylkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_commonfrequencyid"></param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		/// <remarks></remarks>
		public TList<BbPatientdrug> GetByCommonfrequencyid(TransactionManager transactionManager, System.Int32? _commonfrequencyid)
		{
			int count = -1;
			return GetByCommonfrequencyid(transactionManager, _commonfrequencyid, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbCommonFrequencylkp key.
		///		FK_bbPatientdrug_bbCommonFrequencylkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_commonfrequencyid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByCommonfrequencyid(TransactionManager transactionManager, System.Int32? _commonfrequencyid, int start, int pageLength)
		{
			int count = -1;
			return GetByCommonfrequencyid(transactionManager, _commonfrequencyid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbCommonFrequencylkp key.
		///		fkBbPatientdrugBbCommonFrequencylkp Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_commonfrequencyid"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByCommonfrequencyid(System.Int32? _commonfrequencyid, int start, int pageLength)
		{
			int count =  -1;
			return GetByCommonfrequencyid(null, _commonfrequencyid, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbCommonFrequencylkp key.
		///		fkBbPatientdrugBbCommonFrequencylkp Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_commonfrequencyid"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByCommonfrequencyid(System.Int32? _commonfrequencyid, int start, int pageLength,out int count)
		{
			return GetByCommonfrequencyid(null, _commonfrequencyid, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbCommonFrequencylkp key.
		///		FK_bbPatientdrug_bbCommonFrequencylkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_commonfrequencyid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public abstract TList<BbPatientdrug> GetByCommonfrequencyid(TransactionManager transactionManager, System.Int32? _commonfrequencyid, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDrugDoseUnitlkp key.
		///		FK_bbPatientdrug_bbDrugDoseUnitlkp Description: 
		/// </summary>
		/// <param name="_doseunitid"></param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByDoseunitid(System.Int32? _doseunitid)
		{
			int count = -1;
			return GetByDoseunitid(_doseunitid, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDrugDoseUnitlkp key.
		///		FK_bbPatientdrug_bbDrugDoseUnitlkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doseunitid"></param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		/// <remarks></remarks>
		public TList<BbPatientdrug> GetByDoseunitid(TransactionManager transactionManager, System.Int32? _doseunitid)
		{
			int count = -1;
			return GetByDoseunitid(transactionManager, _doseunitid, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDrugDoseUnitlkp key.
		///		FK_bbPatientdrug_bbDrugDoseUnitlkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doseunitid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByDoseunitid(TransactionManager transactionManager, System.Int32? _doseunitid, int start, int pageLength)
		{
			int count = -1;
			return GetByDoseunitid(transactionManager, _doseunitid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDrugDoseUnitlkp key.
		///		fkBbPatientdrugBbDrugDoseUnitlkp Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doseunitid"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByDoseunitid(System.Int32? _doseunitid, int start, int pageLength)
		{
			int count =  -1;
			return GetByDoseunitid(null, _doseunitid, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDrugDoseUnitlkp key.
		///		fkBbPatientdrugBbDrugDoseUnitlkp Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_doseunitid"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByDoseunitid(System.Int32? _doseunitid, int start, int pageLength,out int count)
		{
			return GetByDoseunitid(null, _doseunitid, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDrugDoseUnitlkp key.
		///		FK_bbPatientdrug_bbDrugDoseUnitlkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_doseunitid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public abstract TList<BbPatientdrug> GetByDoseunitid(TransactionManager transactionManager, System.Int32? _doseunitid, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDruglkp key.
		///		FK_bbPatientdrug_bbDruglkp Description: 
		/// </summary>
		/// <param name="_drugid"></param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByDrugid(int _drugid)
		{
			int count = -1;
			return GetByDrugid(_drugid, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDruglkp key.
		///		FK_bbPatientdrug_bbDruglkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_drugid"></param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		/// <remarks></remarks>
		public TList<BbPatientdrug> GetByDrugid(TransactionManager transactionManager, int _drugid)
		{
			int count = -1;
			return GetByDrugid(transactionManager, _drugid, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDruglkp key.
		///		FK_bbPatientdrug_bbDruglkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_drugid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByDrugid(TransactionManager transactionManager, int _drugid, int start, int pageLength)
		{
			int count = -1;
			return GetByDrugid(transactionManager, _drugid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDruglkp key.
		///		fkBbPatientdrugBbDruglkp Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_drugid"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByDrugid(int _drugid, int start, int pageLength)
		{
			int count =  -1;
			return GetByDrugid(null, _drugid, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDruglkp key.
		///		fkBbPatientdrugBbDruglkp Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_drugid"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByDrugid(int _drugid, int start, int pageLength,out int count)
		{
			return GetByDrugid(null, _drugid, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbDruglkp key.
		///		FK_bbPatientdrug_bbDruglkp Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_drugid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public abstract TList<BbPatientdrug> GetByDrugid(TransactionManager transactionManager, int _drugid, int start, int pageLength, out int count);
		
	
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbStopReason key.
		///		FK_bbPatientdrug_bbStopReason Description: 
		/// </summary>
		/// <param name="_stopreasonid"></param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByStopreasonid(System.Int32? _stopreasonid)
		{
			int count = -1;
			return GetByStopreasonid(_stopreasonid, 0,int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbStopReason key.
		///		FK_bbPatientdrug_bbStopReason Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_stopreasonid"></param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		/// <remarks></remarks>
		public TList<BbPatientdrug> GetByStopreasonid(TransactionManager transactionManager, System.Int32? _stopreasonid)
		{
			int count = -1;
			return GetByStopreasonid(transactionManager, _stopreasonid, 0, int.MaxValue, out count);
		}
		
			/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbStopReason key.
		///		FK_bbPatientdrug_bbStopReason Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_stopreasonid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		///  <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByStopreasonid(TransactionManager transactionManager, System.Int32? _stopreasonid, int start, int pageLength)
		{
			int count = -1;
			return GetByStopreasonid(transactionManager, _stopreasonid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbStopReason key.
		///		fkBbPatientdrugBbStopReason Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_stopreasonid"></param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByStopreasonid(System.Int32? _stopreasonid, int start, int pageLength)
		{
			int count =  -1;
			return GetByStopreasonid(null, _stopreasonid, start, pageLength,out count);	
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbStopReason key.
		///		fkBbPatientdrugBbStopReason Description: 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="_stopreasonid"></param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public TList<BbPatientdrug> GetByStopreasonid(System.Int32? _stopreasonid, int start, int pageLength,out int count)
		{
			return GetByStopreasonid(null, _stopreasonid, start, pageLength, out count);	
		}
						
		/// <summary>
		/// 	Gets rows from the datasource based on the FK_bbPatientdrug_bbStopReason key.
		///		FK_bbPatientdrug_bbStopReason Description: 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_stopreasonid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns a typed collection of MHS.Badbir.NetTiers.Entities.BbPatientdrug objects.</returns>
		public abstract TList<BbPatientdrug> GetByStopreasonid(TransactionManager transactionManager, System.Int32? _stopreasonid, int start, int pageLength, out int count);
		
		#endregion

		#region Get By Index Functions
		
		/// <summary>
		/// 	Gets a row from the DataSource based on its primary key.
		/// </summary>
		/// <param name="transactionManager">A <see cref="TransactionManager"/> object.</param>
		/// <param name="key">The unique identifier of the row to retrieve.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <returns>Returns an instance of the Entity class.</returns>
		public override MHS.Badbir.NetTiers.Entities.BbPatientdrug Get(TransactionManager transactionManager, MHS.Badbir.NetTiers.Entities.BbPatientdrugKey key, int start, int pageLength)
		{
			return GetByPatdrugid(transactionManager, key.Patdrugid, start, pageLength);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key Fk_Fupid index.
		/// </summary>
		/// <param name="_fupId">Follow up Id</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByFupId(int _fupId)
		{
			int count = -1;
			return GetByFupId(null,_fupId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the Fk_Fupid index.
		/// </summary>
		/// <param name="_fupId">Follow up Id</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByFupId(int _fupId, int start, int pageLength)
		{
			int count = -1;
			return GetByFupId(null, _fupId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the Fk_Fupid index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_fupId">Follow up Id</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByFupId(TransactionManager transactionManager, int _fupId)
		{
			int count = -1;
			return GetByFupId(transactionManager, _fupId, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the Fk_Fupid index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_fupId">Follow up Id</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByFupId(TransactionManager transactionManager, int _fupId, int start, int pageLength)
		{
			int count = -1;
			return GetByFupId(transactionManager, _fupId, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the Fk_Fupid index.
		/// </summary>
		/// <param name="_fupId">Follow up Id</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByFupId(int _fupId, int start, int pageLength, out int count)
		{
			return GetByFupId(null, _fupId, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the Fk_Fupid index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_fupId">Follow up Id</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public abstract TList<BbPatientdrug> GetByFupId(TransactionManager transactionManager, int _fupId, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key NK_StartYearMonthDayStopYearMonthDay index.
		/// </summary>
		/// <param name="_startyear"></param>
		/// <param name="_startmonth"></param>
		/// <param name="_startday"></param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStartyearStartmonthStartdayStopyearStopmonthStopday(System.Int32? _startyear, System.Int32? _startmonth, System.Int32? _startday, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday)
		{
			int count = -1;
			return GetByStartyearStartmonthStartdayStopyearStopmonthStopday(null,_startyear, _startmonth, _startday, _stopyear, _stopmonth, _stopday, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StartYearMonthDayStopYearMonthDay index.
		/// </summary>
		/// <param name="_startyear"></param>
		/// <param name="_startmonth"></param>
		/// <param name="_startday"></param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStartyearStartmonthStartdayStopyearStopmonthStopday(System.Int32? _startyear, System.Int32? _startmonth, System.Int32? _startday, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday, int start, int pageLength)
		{
			int count = -1;
			return GetByStartyearStartmonthStartdayStopyearStopmonthStopday(null, _startyear, _startmonth, _startday, _stopyear, _stopmonth, _stopday, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StartYearMonthDayStopYearMonthDay index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_startyear"></param>
		/// <param name="_startmonth"></param>
		/// <param name="_startday"></param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStartyearStartmonthStartdayStopyearStopmonthStopday(TransactionManager transactionManager, System.Int32? _startyear, System.Int32? _startmonth, System.Int32? _startday, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday)
		{
			int count = -1;
			return GetByStartyearStartmonthStartdayStopyearStopmonthStopday(transactionManager, _startyear, _startmonth, _startday, _stopyear, _stopmonth, _stopday, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StartYearMonthDayStopYearMonthDay index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_startyear"></param>
		/// <param name="_startmonth"></param>
		/// <param name="_startday"></param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStartyearStartmonthStartdayStopyearStopmonthStopday(TransactionManager transactionManager, System.Int32? _startyear, System.Int32? _startmonth, System.Int32? _startday, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday, int start, int pageLength)
		{
			int count = -1;
			return GetByStartyearStartmonthStartdayStopyearStopmonthStopday(transactionManager, _startyear, _startmonth, _startday, _stopyear, _stopmonth, _stopday, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StartYearMonthDayStopYearMonthDay index.
		/// </summary>
		/// <param name="_startyear"></param>
		/// <param name="_startmonth"></param>
		/// <param name="_startday"></param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStartyearStartmonthStartdayStopyearStopmonthStopday(System.Int32? _startyear, System.Int32? _startmonth, System.Int32? _startday, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday, int start, int pageLength, out int count)
		{
			return GetByStartyearStartmonthStartdayStopyearStopmonthStopday(null, _startyear, _startmonth, _startday, _stopyear, _stopmonth, _stopday, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StartYearMonthDayStopYearMonthDay index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_startyear"></param>
		/// <param name="_startmonth"></param>
		/// <param name="_startday"></param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public abstract TList<BbPatientdrug> GetByStartyearStartmonthStartdayStopyearStopmonthStopday(TransactionManager transactionManager, System.Int32? _startyear, System.Int32? _startmonth, System.Int32? _startday, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key NK_StopYearMonthDay index.
		/// </summary>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStopyearStopmonthStopday(System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday)
		{
			int count = -1;
			return GetByStopyearStopmonthStopday(null,_stopyear, _stopmonth, _stopday, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StopYearMonthDay index.
		/// </summary>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStopyearStopmonthStopday(System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday, int start, int pageLength)
		{
			int count = -1;
			return GetByStopyearStopmonthStopday(null, _stopyear, _stopmonth, _stopday, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StopYearMonthDay index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStopyearStopmonthStopday(TransactionManager transactionManager, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday)
		{
			int count = -1;
			return GetByStopyearStopmonthStopday(transactionManager, _stopyear, _stopmonth, _stopday, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StopYearMonthDay index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStopyearStopmonthStopday(TransactionManager transactionManager, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday, int start, int pageLength)
		{
			int count = -1;
			return GetByStopyearStopmonthStopday(transactionManager, _stopyear, _stopmonth, _stopday, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StopYearMonthDay index.
		/// </summary>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public TList<BbPatientdrug> GetByStopyearStopmonthStopday(System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday, int start, int pageLength, out int count)
		{
			return GetByStopyearStopmonthStopday(null, _stopyear, _stopmonth, _stopday, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the NK_StopYearMonthDay index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_stopyear"></param>
		/// <param name="_stopmonth"></param>
		/// <param name="_stopday"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="TList&lt;BbPatientdrug&gt;"/> class.</returns>
		public abstract TList<BbPatientdrug> GetByStopyearStopmonthStopday(TransactionManager transactionManager, System.Int32? _stopyear, System.Int32? _stopmonth, System.Int32? _stopday, int start, int pageLength, out int count);
						
		/// <summary>
		/// 	Gets rows from the datasource based on the primary key PK_aaPatientdrug index.
		/// </summary>
		/// <param name="_patdrugid"></param>
		/// <returns>Returns an instance of the <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> class.</returns>
		public MHS.Badbir.NetTiers.Entities.BbPatientdrug GetByPatdrugid(int _patdrugid)
		{
			int count = -1;
			return GetByPatdrugid(null,_patdrugid, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_aaPatientdrug index.
		/// </summary>
		/// <param name="_patdrugid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> class.</returns>
		public MHS.Badbir.NetTiers.Entities.BbPatientdrug GetByPatdrugid(int _patdrugid, int start, int pageLength)
		{
			int count = -1;
			return GetByPatdrugid(null, _patdrugid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_aaPatientdrug index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patdrugid"></param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> class.</returns>
		public MHS.Badbir.NetTiers.Entities.BbPatientdrug GetByPatdrugid(TransactionManager transactionManager, int _patdrugid)
		{
			int count = -1;
			return GetByPatdrugid(transactionManager, _patdrugid, 0, int.MaxValue, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_aaPatientdrug index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patdrugid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> class.</returns>
		public MHS.Badbir.NetTiers.Entities.BbPatientdrug GetByPatdrugid(TransactionManager transactionManager, int _patdrugid, int start, int pageLength)
		{
			int count = -1;
			return GetByPatdrugid(transactionManager, _patdrugid, start, pageLength, out count);
		}
		
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_aaPatientdrug index.
		/// </summary>
		/// <param name="_patdrugid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">out parameter to get total records for query</param>
		/// <remarks></remarks>
		/// <returns>Returns an instance of the <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> class.</returns>
		public MHS.Badbir.NetTiers.Entities.BbPatientdrug GetByPatdrugid(int _patdrugid, int start, int pageLength, out int count)
		{
			return GetByPatdrugid(null, _patdrugid, start, pageLength, out count);
		}
		
				
		/// <summary>
		/// 	Gets rows from the datasource based on the PK_aaPatientdrug index.
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="_patdrugid"></param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="count">The total number of records.</param>
		/// <returns>Returns an instance of the <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> class.</returns>
		public abstract MHS.Badbir.NetTiers.Entities.BbPatientdrug GetByPatdrugid(TransactionManager transactionManager, int _patdrugid, int start, int pageLength, out int count);
						
		#endregion "Get By Index Functions"
	
		#region Custom Methods
		
		
		#region bbPatientDrug_AdminUpdateDrugDetails 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminUpdateDrugDetails' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dose"> A <c>System.Double?</c> instance.</param>
		/// <param name="doseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="commonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdose"> A <c>System.Double?</c> instance.</param>
		/// <param name="newdoseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newcommonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateuserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateusername"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminUpdateDrugDetails(System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Double? dose, System.Int32? doseunitid, System.Int32? commonfreqid, System.Int32? newdrugid, System.Int32? newstartday, System.Int32? newstartmonth, System.Int32? newstartyear, System.Double? newdose, System.Int32? newdoseunitid, System.Int32? newcommonfreqid, System.Int32? updateuserid, System.String updateusername)
		{
			 PatientDrug_AdminUpdateDrugDetails(null, 0, int.MaxValue , chid, drugid, startday, startmonth, startyear, dose, doseunitid, commonfreqid, newdrugid, newstartday, newstartmonth, newstartyear, newdose, newdoseunitid, newcommonfreqid, updateuserid, updateusername);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminUpdateDrugDetails' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dose"> A <c>System.Double?</c> instance.</param>
		/// <param name="doseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="commonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdose"> A <c>System.Double?</c> instance.</param>
		/// <param name="newdoseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newcommonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateuserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateusername"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminUpdateDrugDetails(int start, int pageLength, System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Double? dose, System.Int32? doseunitid, System.Int32? commonfreqid, System.Int32? newdrugid, System.Int32? newstartday, System.Int32? newstartmonth, System.Int32? newstartyear, System.Double? newdose, System.Int32? newdoseunitid, System.Int32? newcommonfreqid, System.Int32? updateuserid, System.String updateusername)
		{
			 PatientDrug_AdminUpdateDrugDetails(null, start, pageLength , chid, drugid, startday, startmonth, startyear, dose, doseunitid, commonfreqid, newdrugid, newstartday, newstartmonth, newstartyear, newdose, newdoseunitid, newcommonfreqid, updateuserid, updateusername);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminUpdateDrugDetails' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dose"> A <c>System.Double?</c> instance.</param>
		/// <param name="doseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="commonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdose"> A <c>System.Double?</c> instance.</param>
		/// <param name="newdoseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newcommonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateuserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateusername"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminUpdateDrugDetails(TransactionManager transactionManager, System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Double? dose, System.Int32? doseunitid, System.Int32? commonfreqid, System.Int32? newdrugid, System.Int32? newstartday, System.Int32? newstartmonth, System.Int32? newstartyear, System.Double? newdose, System.Int32? newdoseunitid, System.Int32? newcommonfreqid, System.Int32? updateuserid, System.String updateusername)
		{
			 PatientDrug_AdminUpdateDrugDetails(transactionManager, 0, int.MaxValue , chid, drugid, startday, startmonth, startyear, dose, doseunitid, commonfreqid, newdrugid, newstartday, newstartmonth, newstartyear, newdose, newdoseunitid, newcommonfreqid, updateuserid, updateusername);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminUpdateDrugDetails' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dose"> A <c>System.Double?</c> instance.</param>
		/// <param name="doseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="commonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdose"> A <c>System.Double?</c> instance.</param>
		/// <param name="newdoseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newcommonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateuserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateusername"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void PatientDrug_AdminUpdateDrugDetails(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Double? dose, System.Int32? doseunitid, System.Int32? commonfreqid, System.Int32? newdrugid, System.Int32? newstartday, System.Int32? newstartmonth, System.Int32? newstartyear, System.Double? newdose, System.Int32? newdoseunitid, System.Int32? newcommonfreqid, System.Int32? updateuserid, System.String updateusername);
		
		#endregion
		
		#region bbPharma_AuditSummary 
		
		/// <summary>
		///	This method wrap the 'bbPharma_AuditSummary' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_AuditSummary()
		{
			return Pharma_AuditSummary(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharma_AuditSummary' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_AuditSummary(int start, int pageLength)
		{
			return Pharma_AuditSummary(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbPharma_AuditSummary' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_AuditSummary(TransactionManager transactionManager)
		{
			return Pharma_AuditSummary(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharma_AuditSummary' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Pharma_AuditSummary(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbExport_BStop_Dataset 
		
		/// <summary>
		///	This method wrap the 'bbExport_BStop_Dataset' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Export_BStop_Dataset()
		{
			 Export_BStop_Dataset(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbExport_BStop_Dataset' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Export_BStop_Dataset(int start, int pageLength)
		{
			 Export_BStop_Dataset(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbExport_BStop_Dataset' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Export_BStop_Dataset(TransactionManager transactionManager)
		{
			 Export_BStop_Dataset(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbExport_BStop_Dataset' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void Export_BStop_Dataset(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbMonthwiseRecruitmentReportForCentre_New 
		
		/// <summary>
		///	This method wrap the 'bbMonthwiseRecruitmentReportForCentre_New' stored procedure. 
		/// </summary>
		/// <param name="inputCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet MonthwiseRecruitmentReportForCentre_New(System.Int32? inputCentreId)
		{
			return MonthwiseRecruitmentReportForCentre_New(null, 0, int.MaxValue , inputCentreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbMonthwiseRecruitmentReportForCentre_New' stored procedure. 
		/// </summary>
		/// <param name="inputCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet MonthwiseRecruitmentReportForCentre_New(int start, int pageLength, System.Int32? inputCentreId)
		{
			return MonthwiseRecruitmentReportForCentre_New(null, start, pageLength , inputCentreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbMonthwiseRecruitmentReportForCentre_New' stored procedure. 
		/// </summary>
		/// <param name="inputCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet MonthwiseRecruitmentReportForCentre_New(TransactionManager transactionManager, System.Int32? inputCentreId)
		{
			return MonthwiseRecruitmentReportForCentre_New(transactionManager, 0, int.MaxValue , inputCentreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbMonthwiseRecruitmentReportForCentre_New' stored procedure. 
		/// </summary>
		/// <param name="inputCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet MonthwiseRecruitmentReportForCentre_New(TransactionManager transactionManager, int start, int pageLength , System.Int32? inputCentreId);
		
		#endregion
		
		#region bbSummaryPage_GetUVtherapy 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetUVtherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetUVtherapy(System.Int32? patientid)
		{
			return SummaryPage_GetUVtherapy(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetUVtherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetUVtherapy(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_GetUVtherapy(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetUVtherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetUVtherapy(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_GetUVtherapy(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetUVtherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_GetUVtherapy(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbAdditionalUserDetail_IsUserAdminByID 
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_IsUserAdminByID' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_IsUserAdminByID(System.Int32? badbirUserId)
		{
			return AdditionalUserDetail_IsUserAdminByID(null, 0, int.MaxValue , badbirUserId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_IsUserAdminByID' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_IsUserAdminByID(int start, int pageLength, System.Int32? badbirUserId)
		{
			return AdditionalUserDetail_IsUserAdminByID(null, start, pageLength , badbirUserId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_IsUserAdminByID' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_IsUserAdminByID(TransactionManager transactionManager, System.Int32? badbirUserId)
		{
			return AdditionalUserDetail_IsUserAdminByID(transactionManager, 0, int.MaxValue , badbirUserId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_IsUserAdminByID' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdditionalUserDetail_IsUserAdminByID(TransactionManager transactionManager, int start, int pageLength , System.Int32? badbirUserId);
		
		#endregion
		
		#region BbPharma_MarkPatientAsDeceased 
		
		/// <summary>
		///	This method wrap the 'BbPharma_MarkPatientAsDeceased' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_MarkPatientAsDeceased(System.Int32? patientid, System.Int32? fupaeid)
		{
			return Pharma_MarkPatientAsDeceased(null, 0, int.MaxValue , patientid, fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'BbPharma_MarkPatientAsDeceased' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_MarkPatientAsDeceased(int start, int pageLength, System.Int32? patientid, System.Int32? fupaeid)
		{
			return Pharma_MarkPatientAsDeceased(null, start, pageLength , patientid, fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'BbPharma_MarkPatientAsDeceased' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_MarkPatientAsDeceased(TransactionManager transactionManager, System.Int32? patientid, System.Int32? fupaeid)
		{
			return Pharma_MarkPatientAsDeceased(transactionManager, 0, int.MaxValue , patientid, fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'BbPharma_MarkPatientAsDeceased' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Pharma_MarkPatientAsDeceased(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid, System.Int32? fupaeid);
		
		#endregion
		
		#region bbAdverseEventFup_GetAllAEsByAeLinkageRowID 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetAllAEsByAeLinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetAllAEsByAeLinkageRowID(System.Int32? aeLinkageRowId)
		{
			return AdverseEventFup_GetAllAEsByAeLinkageRowID(null, 0, int.MaxValue , aeLinkageRowId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetAllAEsByAeLinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetAllAEsByAeLinkageRowID(int start, int pageLength, System.Int32? aeLinkageRowId)
		{
			return AdverseEventFup_GetAllAEsByAeLinkageRowID(null, start, pageLength , aeLinkageRowId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetAllAEsByAeLinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetAllAEsByAeLinkageRowID(TransactionManager transactionManager, System.Int32? aeLinkageRowId)
		{
			return AdverseEventFup_GetAllAEsByAeLinkageRowID(transactionManager, 0, int.MaxValue , aeLinkageRowId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetAllAEsByAeLinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_GetAllAEsByAeLinkageRowID(TransactionManager transactionManager, int start, int pageLength , System.Int32? aeLinkageRowId);
		
		#endregion
		
		#region bbCentreDashboard_GetStats 
		
		/// <summary>
		///	This method wrap the 'bbCentreDashboard_GetStats' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreDashboard_GetStats(System.Int32? centreid)
		{
			return CentreDashboard_GetStats(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentreDashboard_GetStats' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreDashboard_GetStats(int start, int pageLength, System.Int32? centreid)
		{
			return CentreDashboard_GetStats(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbCentreDashboard_GetStats' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreDashboard_GetStats(TransactionManager transactionManager, System.Int32? centreid)
		{
			return CentreDashboard_GetStats(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentreDashboard_GetStats' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet CentreDashboard_GetStats(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbAEEsi_FilledLkp 
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_FilledLkp' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_FilledLkp(System.Int32? patientid)
		{
			return AEEsi_FilledLkp(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_FilledLkp' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_FilledLkp(int start, int pageLength, System.Int32? patientid)
		{
			return AEEsi_FilledLkp(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAEEsi_FilledLkp' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_FilledLkp(TransactionManager transactionManager, System.Int32? patientid)
		{
			return AEEsi_FilledLkp(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_FilledLkp' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AEEsi_FilledLkp(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbCloseEditWindows 
		
		/// <summary>
		///	This method wrap the 'bbCloseEditWindows' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CloseEditWindows()
		{
			return CloseEditWindows(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCloseEditWindows' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CloseEditWindows(int start, int pageLength)
		{
			return CloseEditWindows(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbCloseEditWindows' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CloseEditWindows(TransactionManager transactionManager)
		{
			return CloseEditWindows(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCloseEditWindows' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet CloseEditWindows(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbAEEsi_getAllEsisForanAE_ByFupaeid 
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_getAllEsisForanAE_ByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_getAllEsisForanAE_ByFupaeid(System.Int32? fupaeid)
		{
			return AEEsi_getAllEsisForanAE_ByFupaeid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_getAllEsisForanAE_ByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_getAllEsisForanAE_ByFupaeid(int start, int pageLength, System.Int32? fupaeid)
		{
			return AEEsi_getAllEsisForanAE_ByFupaeid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAEEsi_getAllEsisForanAE_ByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_getAllEsisForanAE_ByFupaeid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return AEEsi_getAllEsisForanAE_ByFupaeid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_getAllEsisForanAE_ByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AEEsi_getAllEsisForanAE_ByFupaeid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbPatientDrug_GetPreviousDoses 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetPreviousDoses' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_GetPreviousDoses(System.Int32? patdrugid)
		{
			return PatientDrug_GetPreviousDoses(null, 0, int.MaxValue , patdrugid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetPreviousDoses' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_GetPreviousDoses(int start, int pageLength, System.Int32? patdrugid)
		{
			return PatientDrug_GetPreviousDoses(null, start, pageLength , patdrugid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetPreviousDoses' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_GetPreviousDoses(TransactionManager transactionManager, System.Int32? patdrugid)
		{
			return PatientDrug_GetPreviousDoses(transactionManager, 0, int.MaxValue , patdrugid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetPreviousDoses' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientDrug_GetPreviousDoses(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid);
		
		#endregion
		
		#region bbQuery_UnreadQueryCountByCentre 
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTrainer"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre(System.Int32? centreid, System.Int32? isTrainer)
		{
			return Query_UnreadQueryCountByCentre(null, 0, int.MaxValue , centreid, isTrainer);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTrainer"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre(int start, int pageLength, System.Int32? centreid, System.Int32? isTrainer)
		{
			return Query_UnreadQueryCountByCentre(null, start, pageLength , centreid, isTrainer);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTrainer"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre(TransactionManager transactionManager, System.Int32? centreid, System.Int32? isTrainer)
		{
			return Query_UnreadQueryCountByCentre(transactionManager, 0, int.MaxValue , centreid, isTrainer);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTrainer"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_UnreadQueryCountByCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid, System.Int32? isTrainer);
		
		#endregion
		
		#region bbUserPreRegistration_GetActionRequiredCount 
		
		/// <summary>
		///	This method wrap the 'bbUserPreRegistration_GetActionRequiredCount' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UserPreRegistration_GetActionRequiredCount()
		{
			return UserPreRegistration_GetActionRequiredCount(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbUserPreRegistration_GetActionRequiredCount' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UserPreRegistration_GetActionRequiredCount(int start, int pageLength)
		{
			return UserPreRegistration_GetActionRequiredCount(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbUserPreRegistration_GetActionRequiredCount' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UserPreRegistration_GetActionRequiredCount(TransactionManager transactionManager)
		{
			return UserPreRegistration_GetActionRequiredCount(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbUserPreRegistration_GetActionRequiredCount' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet UserPreRegistration_GetActionRequiredCount(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbAdverseEventFup_getRelatedBioDrugs 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_getRelatedBioDrugs' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_getRelatedBioDrugs(System.Int32? fupid)
		{
			return AdverseEventFup_getRelatedBioDrugs(null, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_getRelatedBioDrugs' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_getRelatedBioDrugs(int start, int pageLength, System.Int32? fupid)
		{
			return AdverseEventFup_getRelatedBioDrugs(null, start, pageLength , fupid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_getRelatedBioDrugs' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_getRelatedBioDrugs(TransactionManager transactionManager, System.Int32? fupid)
		{
			return AdverseEventFup_getRelatedBioDrugs(transactionManager, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_getRelatedBioDrugs' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_getRelatedBioDrugs(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupid);
		
		#endregion
		
		#region bbQuery_UnreadQueryCountByCentre_Baseline 
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Baseline' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Baseline(System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Baseline(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Baseline' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Baseline(int start, int pageLength, System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Baseline(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Baseline' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Baseline(TransactionManager transactionManager, System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Baseline(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Baseline' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_UnreadQueryCountByCentre_Baseline(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbPatientCohorthistory_Generate 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohorthistory_Generate' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="cohortid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="regcentreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="studyno"> A <c>System.String</c> instance.</param>
		/// <param name="createdbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="createdbyname"> A <c>System.String</c> instance.</param>
		/// <param name="createddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
			/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientCohorthistory_Generate(System.Int32? patientid, System.Int32? cohortid, System.DateTime? datefrom, System.Int32? regcentreid, System.String studyno, System.Int32? createdbyid, System.String createdbyname, System.DateTime? createddate, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate, ref System.Int32? chid)
		{
			 PatientCohorthistory_Generate(null, 0, int.MaxValue , patientid, cohortid, datefrom, regcentreid, studyno, createdbyid, createdbyname, createddate, lastupdatedbyid, lastupdatedbyname, lastupdateddate, ref chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohorthistory_Generate' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="cohortid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="regcentreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="studyno"> A <c>System.String</c> instance.</param>
		/// <param name="createdbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="createdbyname"> A <c>System.String</c> instance.</param>
		/// <param name="createddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
			/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientCohorthistory_Generate(int start, int pageLength, System.Int32? patientid, System.Int32? cohortid, System.DateTime? datefrom, System.Int32? regcentreid, System.String studyno, System.Int32? createdbyid, System.String createdbyname, System.DateTime? createddate, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate, ref System.Int32? chid)
		{
			 PatientCohorthistory_Generate(null, start, pageLength , patientid, cohortid, datefrom, regcentreid, studyno, createdbyid, createdbyname, createddate, lastupdatedbyid, lastupdatedbyname, lastupdateddate, ref chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohorthistory_Generate' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="cohortid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="regcentreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="studyno"> A <c>System.String</c> instance.</param>
		/// <param name="createdbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="createdbyname"> A <c>System.String</c> instance.</param>
		/// <param name="createddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
			/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientCohorthistory_Generate(TransactionManager transactionManager, System.Int32? patientid, System.Int32? cohortid, System.DateTime? datefrom, System.Int32? regcentreid, System.String studyno, System.Int32? createdbyid, System.String createdbyname, System.DateTime? createddate, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate, ref System.Int32? chid)
		{
			 PatientCohorthistory_Generate(transactionManager, 0, int.MaxValue , patientid, cohortid, datefrom, regcentreid, studyno, createdbyid, createdbyname, createddate, lastupdatedbyid, lastupdatedbyname, lastupdateddate, ref chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohorthistory_Generate' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="cohortid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="regcentreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="studyno"> A <c>System.String</c> instance.</param>
		/// <param name="createdbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="createdbyname"> A <c>System.String</c> instance.</param>
		/// <param name="createddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
			/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void PatientCohorthistory_Generate(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid, System.Int32? cohortid, System.DateTime? datefrom, System.Int32? regcentreid, System.String studyno, System.Int32? createdbyid, System.String createdbyname, System.DateTime? createddate, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate, ref System.Int32? chid);
		
		#endregion
		
		#region bbQueryForCentre_RecalculateMessageCount 
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_RecalculateMessageCount' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_RecalculateMessageCount(System.Int32? qid, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			return QueryForCentre_RecalculateMessageCount(null, 0, int.MaxValue , qid, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_RecalculateMessageCount' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_RecalculateMessageCount(int start, int pageLength, System.Int32? qid, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			return QueryForCentre_RecalculateMessageCount(null, start, pageLength , qid, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_RecalculateMessageCount' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_RecalculateMessageCount(TransactionManager transactionManager, System.Int32? qid, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			return QueryForCentre_RecalculateMessageCount(transactionManager, 0, int.MaxValue , qid, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_RecalculateMessageCount' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryForCentre_RecalculateMessageCount(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate);
		
		#endregion
		
		#region bbSummaryPage_getAllPasiValues 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getAllPasiValues' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getAllPasiValues(System.Int32? patientid)
		{
			return SummaryPage_getAllPasiValues(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getAllPasiValues' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getAllPasiValues(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_getAllPasiValues(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getAllPasiValues' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getAllPasiValues(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_getAllPasiValues(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getAllPasiValues' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_getAllPasiValues(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbPatientGraphsData 
		
		/// <summary>
		///	This method wrap the 'bbPatientGraphsData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientGraphsData(System.Int32? patientid)
		{
			return PatientGraphsData(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientGraphsData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientGraphsData(int start, int pageLength, System.Int32? patientid)
		{
			return PatientGraphsData(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientGraphsData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientGraphsData(TransactionManager transactionManager, System.Int32? patientid)
		{
			return PatientGraphsData(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientGraphsData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientGraphsData(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region BbAdminTool_GetNonLoggingUsers 
		
		/// <summary>
		///	This method wrap the 'BbAdminTool_GetNonLoggingUsers' stored procedure. 
		/// </summary>
		/// <param name="months"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdminTool_GetNonLoggingUsers(System.Int32? months)
		{
			return AdminTool_GetNonLoggingUsers(null, 0, int.MaxValue , months);
		}
		
		/// <summary>
		///	This method wrap the 'BbAdminTool_GetNonLoggingUsers' stored procedure. 
		/// </summary>
		/// <param name="months"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdminTool_GetNonLoggingUsers(int start, int pageLength, System.Int32? months)
		{
			return AdminTool_GetNonLoggingUsers(null, start, pageLength , months);
		}
				
		/// <summary>
		///	This method wrap the 'BbAdminTool_GetNonLoggingUsers' stored procedure. 
		/// </summary>
		/// <param name="months"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdminTool_GetNonLoggingUsers(TransactionManager transactionManager, System.Int32? months)
		{
			return AdminTool_GetNonLoggingUsers(transactionManager, 0, int.MaxValue , months);
		}
		
		/// <summary>
		///	This method wrap the 'BbAdminTool_GetNonLoggingUsers' stored procedure. 
		/// </summary>
		/// <param name="months"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdminTool_GetNonLoggingUsers(TransactionManager transactionManager, int start, int pageLength , System.Int32? months);
		
		#endregion
		
		#region bbAdditionalUserDetail_GetAllUsers_Custom 
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_GetAllUsers_Custom' stored procedure. 
		/// </summary>
		/// <param name="showAllUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showValidUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showActiveUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showLockedOutUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showPreRegisteredUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showCliniciansOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showCentreRdOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showAdminsOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showUsersWithCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_GetAllUsers_Custom(System.Int32? showAllUsers, System.Int32? showValidUsers, System.Int32? showActiveUsers, System.Int32? showLockedOutUsers, System.Int32? showPreRegisteredUsers, System.Int32? showCliniciansOnly, System.Int32? showCentreRdOnly, System.Int32? showAdminsOnly, System.Int32? showUsersWithCentreId)
		{
			return AdditionalUserDetail_GetAllUsers_Custom(null, 0, int.MaxValue , showAllUsers, showValidUsers, showActiveUsers, showLockedOutUsers, showPreRegisteredUsers, showCliniciansOnly, showCentreRdOnly, showAdminsOnly, showUsersWithCentreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_GetAllUsers_Custom' stored procedure. 
		/// </summary>
		/// <param name="showAllUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showValidUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showActiveUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showLockedOutUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showPreRegisteredUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showCliniciansOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showCentreRdOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showAdminsOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showUsersWithCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_GetAllUsers_Custom(int start, int pageLength, System.Int32? showAllUsers, System.Int32? showValidUsers, System.Int32? showActiveUsers, System.Int32? showLockedOutUsers, System.Int32? showPreRegisteredUsers, System.Int32? showCliniciansOnly, System.Int32? showCentreRdOnly, System.Int32? showAdminsOnly, System.Int32? showUsersWithCentreId)
		{
			return AdditionalUserDetail_GetAllUsers_Custom(null, start, pageLength , showAllUsers, showValidUsers, showActiveUsers, showLockedOutUsers, showPreRegisteredUsers, showCliniciansOnly, showCentreRdOnly, showAdminsOnly, showUsersWithCentreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_GetAllUsers_Custom' stored procedure. 
		/// </summary>
		/// <param name="showAllUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showValidUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showActiveUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showLockedOutUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showPreRegisteredUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showCliniciansOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showCentreRdOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showAdminsOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showUsersWithCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_GetAllUsers_Custom(TransactionManager transactionManager, System.Int32? showAllUsers, System.Int32? showValidUsers, System.Int32? showActiveUsers, System.Int32? showLockedOutUsers, System.Int32? showPreRegisteredUsers, System.Int32? showCliniciansOnly, System.Int32? showCentreRdOnly, System.Int32? showAdminsOnly, System.Int32? showUsersWithCentreId)
		{
			return AdditionalUserDetail_GetAllUsers_Custom(transactionManager, 0, int.MaxValue , showAllUsers, showValidUsers, showActiveUsers, showLockedOutUsers, showPreRegisteredUsers, showCliniciansOnly, showCentreRdOnly, showAdminsOnly, showUsersWithCentreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_GetAllUsers_Custom' stored procedure. 
		/// </summary>
		/// <param name="showAllUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showValidUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showActiveUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showLockedOutUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showPreRegisteredUsers"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showCliniciansOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showCentreRdOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showAdminsOnly"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showUsersWithCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdditionalUserDetail_GetAllUsers_Custom(TransactionManager transactionManager, int start, int pageLength , System.Int32? showAllUsers, System.Int32? showValidUsers, System.Int32? showActiveUsers, System.Int32? showLockedOutUsers, System.Int32? showPreRegisteredUsers, System.Int32? showCliniciansOnly, System.Int32? showCentreRdOnly, System.Int32? showAdminsOnly, System.Int32? showUsersWithCentreId);
		
		#endregion
		
		#region bbDruglkp_Get_List_Current_For_patdrugid 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Current_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="cohort"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Current_For_patdrugid(System.Int32? patdrugid, System.Int32? cohort)
		{
			return Druglkp_Get_List_Current_For_patdrugid(null, 0, int.MaxValue , patdrugid, cohort);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Current_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="cohort"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Current_For_patdrugid(int start, int pageLength, System.Int32? patdrugid, System.Int32? cohort)
		{
			return Druglkp_Get_List_Current_For_patdrugid(null, start, pageLength , patdrugid, cohort);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Current_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="cohort"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Current_For_patdrugid(TransactionManager transactionManager, System.Int32? patdrugid, System.Int32? cohort)
		{
			return Druglkp_Get_List_Current_For_patdrugid(transactionManager, 0, int.MaxValue , patdrugid, cohort);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Current_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="cohort"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Current_For_patdrugid(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid, System.Int32? cohort);
		
		#endregion
		
		#region bbDruglkp_Get_List_Biologic_For_patdrugid_Baseline 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic_For_patdrugid_Baseline(System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Biologic_For_patdrugid_Baseline(null, 0, int.MaxValue , patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic_For_patdrugid_Baseline(int start, int pageLength, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Biologic_For_patdrugid_Baseline(null, start, pageLength , patdrugid, isAdmin);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic_For_patdrugid_Baseline(TransactionManager transactionManager, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Biologic_For_patdrugid_Baseline(transactionManager, 0, int.MaxValue , patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Biologic_For_patdrugid_Baseline(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid, System.Int32? isAdmin);
		
		#endregion
		
		#region bbPatient_GetConsentObtainerList 
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetConsentObtainerList' stored procedure. 
		/// </summary>
		/// <param name="consentDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetConsentObtainerList(System.DateTime? consentDate, System.Int32? centreid)
		{
			return Patient_GetConsentObtainerList(null, 0, int.MaxValue , consentDate, centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetConsentObtainerList' stored procedure. 
		/// </summary>
		/// <param name="consentDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetConsentObtainerList(int start, int pageLength, System.DateTime? consentDate, System.Int32? centreid)
		{
			return Patient_GetConsentObtainerList(null, start, pageLength , consentDate, centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_GetConsentObtainerList' stored procedure. 
		/// </summary>
		/// <param name="consentDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetConsentObtainerList(TransactionManager transactionManager, System.DateTime? consentDate, System.Int32? centreid)
		{
			return Patient_GetConsentObtainerList(transactionManager, 0, int.MaxValue , consentDate, centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetConsentObtainerList' stored procedure. 
		/// </summary>
		/// <param name="consentDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_GetConsentObtainerList(TransactionManager transactionManager, int start, int pageLength , System.DateTime? consentDate, System.Int32? centreid);
		
		#endregion
		
		#region bbQuery_GetPatientUnsolvedQueryCount 
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetPatientUnsolvedQueryCount' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetPatientUnsolvedQueryCount(System.Int32? chid)
		{
			return Query_GetPatientUnsolvedQueryCount(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetPatientUnsolvedQueryCount' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetPatientUnsolvedQueryCount(int start, int pageLength, System.Int32? chid)
		{
			return Query_GetPatientUnsolvedQueryCount(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_GetPatientUnsolvedQueryCount' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetPatientUnsolvedQueryCount(TransactionManager transactionManager, System.Int32? chid)
		{
			return Query_GetPatientUnsolvedQueryCount(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetPatientUnsolvedQueryCount' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_GetPatientUnsolvedQueryCount(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbDruglkp_Get_List_Previous_For_patdrugid 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Previous_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Previous_For_patdrugid(System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Previous_For_patdrugid(null, 0, int.MaxValue , patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Previous_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Previous_For_patdrugid(int start, int pageLength, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Previous_For_patdrugid(null, start, pageLength , patdrugid, isAdmin);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Previous_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Previous_For_patdrugid(TransactionManager transactionManager, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Previous_For_patdrugid(transactionManager, 0, int.MaxValue , patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Previous_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Previous_For_patdrugid(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid, System.Int32? isAdmin);
		
		#endregion
		
		#region bbGetRecordsinEditWindow 
		
		/// <summary>
		///	This method wrap the 'bbGetRecordsinEditWindow' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetRecordsinEditWindow(System.Int32? centreId)
		{
			return GetRecordsinEditWindow(null, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbGetRecordsinEditWindow' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetRecordsinEditWindow(int start, int pageLength, System.Int32? centreId)
		{
			return GetRecordsinEditWindow(null, start, pageLength , centreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbGetRecordsinEditWindow' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetRecordsinEditWindow(TransactionManager transactionManager, System.Int32? centreId)
		{
			return GetRecordsinEditWindow(transactionManager, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbGetRecordsinEditWindow' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet GetRecordsinEditWindow(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreId);
		
		#endregion
		
		#region bbPatientdrug_GetByFupId_Conventional 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Conventional' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Conventional(System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Conventional(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Conventional' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Conventional(int start, int pageLength, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Conventional(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Conventional' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Conventional(TransactionManager transactionManager, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Conventional(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Conventional' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public abstract TList<BbPatientdrug> Patientdrug_GetByFupId_Conventional(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbPatientdrug_GetByFupId_Current 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Current' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Current(System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Current(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Current' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Current(int start, int pageLength, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Current(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Current' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Current(TransactionManager transactionManager, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Current(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Current' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public abstract TList<BbPatientdrug> Patientdrug_GetByFupId_Current(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbAdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID(System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID(null, 0, int.MaxValue , aeLinkageRowId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID(int start, int pageLength, System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID(null, start, pageLength , aeLinkageRowId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID(TransactionManager transactionManager, System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID(transactionManager, 0, int.MaxValue , aeLinkageRowId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventLinkageProcessingOperation_GetAllInstancesByAELinkageRowID(TransactionManager transactionManager, int start, int pageLength , System.Int32? aeLinkageRowId);
		
		#endregion
		
		#region bbCommittee_RegionalChartsData 
		
		/// <summary>
		///	This method wrap the 'bbCommittee_RegionalChartsData' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_RegionalChartsData()
		{
			return Committee_RegionalChartsData(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCommittee_RegionalChartsData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_RegionalChartsData(int start, int pageLength)
		{
			return Committee_RegionalChartsData(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbCommittee_RegionalChartsData' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_RegionalChartsData(TransactionManager transactionManager)
		{
			return Committee_RegionalChartsData(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCommittee_RegionalChartsData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Committee_RegionalChartsData(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbPatientDrug_AdminDeleteConMeds 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminDeleteConMeds' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminDeleteConMeds(System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear)
		{
			 PatientDrug_AdminDeleteConMeds(null, 0, int.MaxValue , chid, drugid, startday, startmonth, startyear);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminDeleteConMeds' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminDeleteConMeds(int start, int pageLength, System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear)
		{
			 PatientDrug_AdminDeleteConMeds(null, start, pageLength , chid, drugid, startday, startmonth, startyear);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminDeleteConMeds' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminDeleteConMeds(TransactionManager transactionManager, System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear)
		{
			 PatientDrug_AdminDeleteConMeds(transactionManager, 0, int.MaxValue , chid, drugid, startday, startmonth, startyear);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminDeleteConMeds' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void PatientDrug_AdminDeleteConMeds(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear);
		
		#endregion
		
		#region bbDelegationLog_mergeAnonymousAccountWithNewAccount 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_mergeAnonymousAccountWithNewAccount' stored procedure. 
		/// </summary>
		/// <param name="oldBadbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newBadbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_mergeAnonymousAccountWithNewAccount(System.Int32? oldBadbirUserId, System.Int32? newBadbirUserId)
		{
			return DelegationLog_mergeAnonymousAccountWithNewAccount(null, 0, int.MaxValue , oldBadbirUserId, newBadbirUserId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_mergeAnonymousAccountWithNewAccount' stored procedure. 
		/// </summary>
		/// <param name="oldBadbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newBadbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_mergeAnonymousAccountWithNewAccount(int start, int pageLength, System.Int32? oldBadbirUserId, System.Int32? newBadbirUserId)
		{
			return DelegationLog_mergeAnonymousAccountWithNewAccount(null, start, pageLength , oldBadbirUserId, newBadbirUserId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLog_mergeAnonymousAccountWithNewAccount' stored procedure. 
		/// </summary>
		/// <param name="oldBadbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newBadbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_mergeAnonymousAccountWithNewAccount(TransactionManager transactionManager, System.Int32? oldBadbirUserId, System.Int32? newBadbirUserId)
		{
			return DelegationLog_mergeAnonymousAccountWithNewAccount(transactionManager, 0, int.MaxValue , oldBadbirUserId, newBadbirUserId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_mergeAnonymousAccountWithNewAccount' stored procedure. 
		/// </summary>
		/// <param name="oldBadbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newBadbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLog_mergeAnonymousAccountWithNewAccount(TransactionManager transactionManager, int start, int pageLength , System.Int32? oldBadbirUserId, System.Int32? newBadbirUserId);
		
		#endregion
		
		#region bbPatient_GetLastFilledBioFupByPatientId 
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetLastFilledBioFupByPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetLastFilledBioFupByPatientId(System.Int32? patientid)
		{
			return Patient_GetLastFilledBioFupByPatientId(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetLastFilledBioFupByPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetLastFilledBioFupByPatientId(int start, int pageLength, System.Int32? patientid)
		{
			return Patient_GetLastFilledBioFupByPatientId(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_GetLastFilledBioFupByPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetLastFilledBioFupByPatientId(TransactionManager transactionManager, System.Int32? patientid)
		{
			return Patient_GetLastFilledBioFupByPatientId(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetLastFilledBioFupByPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_GetLastFilledBioFupByPatientId(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbQuery_GetByPatientID_ExcludeSolvedAndExcluded 
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByPatientID_ExcludeSolvedAndExcluded' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByPatientID_ExcludeSolvedAndExcluded(System.Int32? patientid)
		{
			return Query_GetByPatientID_ExcludeSolvedAndExcluded(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByPatientID_ExcludeSolvedAndExcluded' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByPatientID_ExcludeSolvedAndExcluded(int start, int pageLength, System.Int32? patientid)
		{
			return Query_GetByPatientID_ExcludeSolvedAndExcluded(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_GetByPatientID_ExcludeSolvedAndExcluded' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByPatientID_ExcludeSolvedAndExcluded(TransactionManager transactionManager, System.Int32? patientid)
		{
			return Query_GetByPatientID_ExcludeSolvedAndExcluded(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByPatientID_ExcludeSolvedAndExcluded' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_GetByPatientID_ExcludeSolvedAndExcluded(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbAdverseEvent_GetByChid 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEvent_GetByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEvent_GetByChid(System.Int32? chid)
		{
			return AdverseEvent_GetByChid(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEvent_GetByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEvent_GetByChid(int start, int pageLength, System.Int32? chid)
		{
			return AdverseEvent_GetByChid(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEvent_GetByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEvent_GetByChid(TransactionManager transactionManager, System.Int32? chid)
		{
			return AdverseEvent_GetByChid(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEvent_GetByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEvent_GetByChid(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbAdverseEventFup_MarkSupervised 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_MarkSupervised' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="supervisorid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="supervisorname"> A <c>System.String</c> instance.</param>
		/// <param name="superviseddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="superviseresult"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_MarkSupervised(System.Int32? fupaeid, System.Int32? supervisorid, System.String supervisorname, System.DateTime? superviseddate, System.Int32? superviseresult)
		{
			return AdverseEventFup_MarkSupervised(null, 0, int.MaxValue , fupaeid, supervisorid, supervisorname, superviseddate, superviseresult);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_MarkSupervised' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="supervisorid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="supervisorname"> A <c>System.String</c> instance.</param>
		/// <param name="superviseddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="superviseresult"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_MarkSupervised(int start, int pageLength, System.Int32? fupaeid, System.Int32? supervisorid, System.String supervisorname, System.DateTime? superviseddate, System.Int32? superviseresult)
		{
			return AdverseEventFup_MarkSupervised(null, start, pageLength , fupaeid, supervisorid, supervisorname, superviseddate, superviseresult);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_MarkSupervised' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="supervisorid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="supervisorname"> A <c>System.String</c> instance.</param>
		/// <param name="superviseddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="superviseresult"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_MarkSupervised(TransactionManager transactionManager, System.Int32? fupaeid, System.Int32? supervisorid, System.String supervisorname, System.DateTime? superviseddate, System.Int32? superviseresult)
		{
			return AdverseEventFup_MarkSupervised(transactionManager, 0, int.MaxValue , fupaeid, supervisorid, supervisorname, superviseddate, superviseresult);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_MarkSupervised' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="supervisorid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="supervisorname"> A <c>System.String</c> instance.</param>
		/// <param name="superviseddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="superviseresult"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_MarkSupervised(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid, System.Int32? supervisorid, System.String supervisorname, System.DateTime? superviseddate, System.Int32? superviseresult);
		
		#endregion
		
		#region bbCentreAuditSummary_GetByCentreid 
		
		/// <summary>
		///	This method wrap the 'bbCentreAuditSummary_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreAuditSummary_GetByCentreid(System.Int32? centreid)
		{
			return CentreAuditSummary_GetByCentreid(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentreAuditSummary_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreAuditSummary_GetByCentreid(int start, int pageLength, System.Int32? centreid)
		{
			return CentreAuditSummary_GetByCentreid(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbCentreAuditSummary_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreAuditSummary_GetByCentreid(TransactionManager transactionManager, System.Int32? centreid)
		{
			return CentreAuditSummary_GetByCentreid(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentreAuditSummary_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet CentreAuditSummary_GetByCentreid(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbPatientCohortSwitch_copyComorbidities 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_copyComorbidities' stored procedure. 
		/// </summary>
		/// <param name="conventionalChid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="biologicChid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_copyComorbidities(System.Int32? conventionalChid, System.Int32? biologicChid)
		{
			return PatientCohortSwitch_copyComorbidities(null, 0, int.MaxValue , conventionalChid, biologicChid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_copyComorbidities' stored procedure. 
		/// </summary>
		/// <param name="conventionalChid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="biologicChid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_copyComorbidities(int start, int pageLength, System.Int32? conventionalChid, System.Int32? biologicChid)
		{
			return PatientCohortSwitch_copyComorbidities(null, start, pageLength , conventionalChid, biologicChid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_copyComorbidities' stored procedure. 
		/// </summary>
		/// <param name="conventionalChid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="biologicChid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_copyComorbidities(TransactionManager transactionManager, System.Int32? conventionalChid, System.Int32? biologicChid)
		{
			return PatientCohortSwitch_copyComorbidities(transactionManager, 0, int.MaxValue , conventionalChid, biologicChid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_copyComorbidities' stored procedure. 
		/// </summary>
		/// <param name="conventionalChid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="biologicChid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortSwitch_copyComorbidities(TransactionManager transactionManager, int start, int pageLength , System.Int32? conventionalChid, System.Int32? biologicChid);
		
		#endregion
		
		#region bbQuery_MarkAsAdminRead 
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsAdminRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsAdminRead(System.Int32? qid)
		{
			return Query_MarkAsAdminRead(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsAdminRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsAdminRead(int start, int pageLength, System.Int32? qid)
		{
			return Query_MarkAsAdminRead(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsAdminRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsAdminRead(TransactionManager transactionManager, System.Int32? qid)
		{
			return Query_MarkAsAdminRead(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsAdminRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_MarkAsAdminRead(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbPatientdrug_GetByFupId_SmallMolecule 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_SmallMolecule' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_SmallMolecule(System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_SmallMolecule(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_SmallMolecule' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_SmallMolecule(int start, int pageLength, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_SmallMolecule(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_SmallMolecule' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_SmallMolecule(TransactionManager transactionManager, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_SmallMolecule(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_SmallMolecule' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public abstract TList<BbPatientdrug> Patientdrug_GetByFupId_SmallMolecule(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbCentre_GetByBADBIRUserid 
		
		/// <summary>
		///	This method wrap the 'bbCentre_GetByBADBIRUserid' stored procedure. 
		/// </summary>
		/// <param name="badbirUserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_GetByBADBIRUserid(System.Int32? badbirUserid, System.Int32? isTraining)
		{
			return Centre_GetByBADBIRUserid(null, 0, int.MaxValue , badbirUserid, isTraining);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentre_GetByBADBIRUserid' stored procedure. 
		/// </summary>
		/// <param name="badbirUserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_GetByBADBIRUserid(int start, int pageLength, System.Int32? badbirUserid, System.Int32? isTraining)
		{
			return Centre_GetByBADBIRUserid(null, start, pageLength , badbirUserid, isTraining);
		}
				
		/// <summary>
		///	This method wrap the 'bbCentre_GetByBADBIRUserid' stored procedure. 
		/// </summary>
		/// <param name="badbirUserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_GetByBADBIRUserid(TransactionManager transactionManager, System.Int32? badbirUserid, System.Int32? isTraining)
		{
			return Centre_GetByBADBIRUserid(transactionManager, 0, int.MaxValue , badbirUserid, isTraining);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentre_GetByBADBIRUserid' stored procedure. 
		/// </summary>
		/// <param name="badbirUserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Centre_GetByBADBIRUserid(TransactionManager transactionManager, int start, int pageLength , System.Int32? badbirUserid, System.Int32? isTraining);
		
		#endregion
		
		#region bbPharmaLog_GetByAEUID 
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByAEUID' stored procedure. 
		/// </summary>
		/// <param name="aeuid"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByAEUID(System.String aeuid)
		{
			return PharmaLog_GetByAEUID(null, 0, int.MaxValue , aeuid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByAEUID' stored procedure. 
		/// </summary>
		/// <param name="aeuid"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByAEUID(int start, int pageLength, System.String aeuid)
		{
			return PharmaLog_GetByAEUID(null, start, pageLength , aeuid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByAEUID' stored procedure. 
		/// </summary>
		/// <param name="aeuid"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByAEUID(TransactionManager transactionManager, System.String aeuid)
		{
			return PharmaLog_GetByAEUID(transactionManager, 0, int.MaxValue , aeuid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByAEUID' stored procedure. 
		/// </summary>
		/// <param name="aeuid"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PharmaLog_GetByAEUID(TransactionManager transactionManager, int start, int pageLength , System.String aeuid);
		
		#endregion
		
		#region bbUpdateAELock 
		
		/// <summary>
		///	This method wrap the 'bbUpdateAELock' stored procedure. 
		/// </summary>
		/// <param name="locked"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="datepvaware"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="pvusername"> A <c>System.String</c> instance.</param>
		/// <param name="originalFupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateAELock(System.Boolean? locked, System.DateTime? datepvaware, System.String pvusername, System.Int32? originalFupaeid)
		{
			 UpdateAELock(null, 0, int.MaxValue , locked, datepvaware, pvusername, originalFupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbUpdateAELock' stored procedure. 
		/// </summary>
		/// <param name="locked"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="datepvaware"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="pvusername"> A <c>System.String</c> instance.</param>
		/// <param name="originalFupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateAELock(int start, int pageLength, System.Boolean? locked, System.DateTime? datepvaware, System.String pvusername, System.Int32? originalFupaeid)
		{
			 UpdateAELock(null, start, pageLength , locked, datepvaware, pvusername, originalFupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbUpdateAELock' stored procedure. 
		/// </summary>
		/// <param name="locked"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="datepvaware"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="pvusername"> A <c>System.String</c> instance.</param>
		/// <param name="originalFupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateAELock(TransactionManager transactionManager, System.Boolean? locked, System.DateTime? datepvaware, System.String pvusername, System.Int32? originalFupaeid)
		{
			 UpdateAELock(transactionManager, 0, int.MaxValue , locked, datepvaware, pvusername, originalFupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbUpdateAELock' stored procedure. 
		/// </summary>
		/// <param name="locked"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="datepvaware"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="pvusername"> A <c>System.String</c> instance.</param>
		/// <param name="originalFupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void UpdateAELock(TransactionManager transactionManager, int start, int pageLength , System.Boolean? locked, System.DateTime? datepvaware, System.String pvusername, System.Int32? originalFupaeid);
		
		#endregion
		
		#region bbQueryTypelkp_GetAllForPV 
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForPV' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForPV()
		{
			return QueryTypelkp_GetAllForPV(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForPV' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForPV(int start, int pageLength)
		{
			return QueryTypelkp_GetAllForPV(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForPV' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForPV(TransactionManager transactionManager)
		{
			return QueryTypelkp_GetAllForPV(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForPV' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryTypelkp_GetAllForPV(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbFileStorage_GetByPatientID 
		
		/// <summary>
		///	This method wrap the 'bbFileStorage_GetByPatientID' stored procedure. 
		/// </summary>
		/// <param name="patientId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet FileStorage_GetByPatientID(System.Int32? patientId)
		{
			return FileStorage_GetByPatientID(null, 0, int.MaxValue , patientId);
		}
		
		/// <summary>
		///	This method wrap the 'bbFileStorage_GetByPatientID' stored procedure. 
		/// </summary>
		/// <param name="patientId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet FileStorage_GetByPatientID(int start, int pageLength, System.Int32? patientId)
		{
			return FileStorage_GetByPatientID(null, start, pageLength , patientId);
		}
				
		/// <summary>
		///	This method wrap the 'bbFileStorage_GetByPatientID' stored procedure. 
		/// </summary>
		/// <param name="patientId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet FileStorage_GetByPatientID(TransactionManager transactionManager, System.Int32? patientId)
		{
			return FileStorage_GetByPatientID(transactionManager, 0, int.MaxValue , patientId);
		}
		
		/// <summary>
		///	This method wrap the 'bbFileStorage_GetByPatientID' stored procedure. 
		/// </summary>
		/// <param name="patientId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet FileStorage_GetByPatientID(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientId);
		
		#endregion
		
		#region bbDruglkp_Get_List_SM_For_patdrugid_Fup 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM_For_patdrugid_Fup' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM_For_patdrugid_Fup(System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_SM_For_patdrugid_Fup(null, 0, int.MaxValue , patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM_For_patdrugid_Fup' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM_For_patdrugid_Fup(int start, int pageLength, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_SM_For_patdrugid_Fup(null, start, pageLength , patdrugid, isAdmin);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM_For_patdrugid_Fup' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM_For_patdrugid_Fup(TransactionManager transactionManager, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_SM_For_patdrugid_Fup(transactionManager, 0, int.MaxValue , patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM_For_patdrugid_Fup' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_SM_For_patdrugid_Fup(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid, System.Int32? isAdmin);
		
		#endregion
		
		#region bbDelegationLog_getAuthorizedUsersForCentre 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_getAuthorizedUsersForCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_getAuthorizedUsersForCentre(System.Int32? centreid)
		{
			return DelegationLog_getAuthorizedUsersForCentre(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_getAuthorizedUsersForCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_getAuthorizedUsersForCentre(int start, int pageLength, System.Int32? centreid)
		{
			return DelegationLog_getAuthorizedUsersForCentre(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLog_getAuthorizedUsersForCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_getAuthorizedUsersForCentre(TransactionManager transactionManager, System.Int32? centreid)
		{
			return DelegationLog_getAuthorizedUsersForCentre(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_getAuthorizedUsersForCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLog_getAuthorizedUsersForCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbPatientComorbidities_getAllComorbiditiesByFupaeid 
		
		/// <summary>
		///	This method wrap the 'bbPatientComorbidities_getAllComorbiditiesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientComorbidities_getAllComorbiditiesByFupaeid(System.Int32? fupaeid)
		{
			return PatientComorbidities_getAllComorbiditiesByFupaeid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientComorbidities_getAllComorbiditiesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientComorbidities_getAllComorbiditiesByFupaeid(int start, int pageLength, System.Int32? fupaeid)
		{
			return PatientComorbidities_getAllComorbiditiesByFupaeid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientComorbidities_getAllComorbiditiesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientComorbidities_getAllComorbiditiesByFupaeid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return PatientComorbidities_getAllComorbiditiesByFupaeid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientComorbidities_getAllComorbiditiesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientComorbidities_getAllComorbiditiesByFupaeid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbAdverseEventStatusSummary 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventStatusSummary' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventStatusSummary()
		{
			return AdverseEventStatusSummary(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventStatusSummary' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventStatusSummary(int start, int pageLength)
		{
			return AdverseEventStatusSummary(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventStatusSummary' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventStatusSummary(TransactionManager transactionManager)
		{
			return AdverseEventStatusSummary(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventStatusSummary' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventStatusSummary(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbAdverseEventAll_getAllOtherEventsByFupaeid 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventAll_getAllOtherEventsByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventAll_getAllOtherEventsByFupaeid(System.Int32? fupaeid)
		{
			return AdverseEventAll_getAllOtherEventsByFupaeid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventAll_getAllOtherEventsByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventAll_getAllOtherEventsByFupaeid(int start, int pageLength, System.Int32? fupaeid)
		{
			return AdverseEventAll_getAllOtherEventsByFupaeid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventAll_getAllOtherEventsByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventAll_getAllOtherEventsByFupaeid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return AdverseEventAll_getAllOtherEventsByFupaeid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventAll_getAllOtherEventsByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventAll_getAllOtherEventsByFupaeid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbPharmaLog_Insert 
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_Insert' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupcode"> A <c>System.Byte?</c> instance.</param>
		/// <param name="aeuid"> A <c>System.String</c> instance.</param>
		/// <param name="logTypeId"> A <c>System.Int16?</c> instance.</param>
		/// <param name="logDetail"> A <c>System.String</c> instance.</param>
		/// <param name="createdById"> A <c>System.Int16?</c> instance.</param>
		/// <param name="createdByName"> A <c>System.String</c> instance.</param>
		/// <param name="createdByDate"> A <c>System.DateTime?</c> instance.</param>
			/// <param name="logId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PharmaLog_Insert(System.Int32? chid, System.Byte? fupcode, System.String aeuid, System.Int16? logTypeId, System.String logDetail, System.Int16? createdById, System.String createdByName, System.DateTime? createdByDate, ref System.Int32? logId)
		{
			 PharmaLog_Insert(null, 0, int.MaxValue , chid, fupcode, aeuid, logTypeId, logDetail, createdById, createdByName, createdByDate, ref logId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_Insert' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupcode"> A <c>System.Byte?</c> instance.</param>
		/// <param name="aeuid"> A <c>System.String</c> instance.</param>
		/// <param name="logTypeId"> A <c>System.Int16?</c> instance.</param>
		/// <param name="logDetail"> A <c>System.String</c> instance.</param>
		/// <param name="createdById"> A <c>System.Int16?</c> instance.</param>
		/// <param name="createdByName"> A <c>System.String</c> instance.</param>
		/// <param name="createdByDate"> A <c>System.DateTime?</c> instance.</param>
			/// <param name="logId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PharmaLog_Insert(int start, int pageLength, System.Int32? chid, System.Byte? fupcode, System.String aeuid, System.Int16? logTypeId, System.String logDetail, System.Int16? createdById, System.String createdByName, System.DateTime? createdByDate, ref System.Int32? logId)
		{
			 PharmaLog_Insert(null, start, pageLength , chid, fupcode, aeuid, logTypeId, logDetail, createdById, createdByName, createdByDate, ref logId);
		}
				
		/// <summary>
		///	This method wrap the 'bbPharmaLog_Insert' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupcode"> A <c>System.Byte?</c> instance.</param>
		/// <param name="aeuid"> A <c>System.String</c> instance.</param>
		/// <param name="logTypeId"> A <c>System.Int16?</c> instance.</param>
		/// <param name="logDetail"> A <c>System.String</c> instance.</param>
		/// <param name="createdById"> A <c>System.Int16?</c> instance.</param>
		/// <param name="createdByName"> A <c>System.String</c> instance.</param>
		/// <param name="createdByDate"> A <c>System.DateTime?</c> instance.</param>
			/// <param name="logId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PharmaLog_Insert(TransactionManager transactionManager, System.Int32? chid, System.Byte? fupcode, System.String aeuid, System.Int16? logTypeId, System.String logDetail, System.Int16? createdById, System.String createdByName, System.DateTime? createdByDate, ref System.Int32? logId)
		{
			 PharmaLog_Insert(transactionManager, 0, int.MaxValue , chid, fupcode, aeuid, logTypeId, logDetail, createdById, createdByName, createdByDate, ref logId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_Insert' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupcode"> A <c>System.Byte?</c> instance.</param>
		/// <param name="aeuid"> A <c>System.String</c> instance.</param>
		/// <param name="logTypeId"> A <c>System.Int16?</c> instance.</param>
		/// <param name="logDetail"> A <c>System.String</c> instance.</param>
		/// <param name="createdById"> A <c>System.Int16?</c> instance.</param>
		/// <param name="createdByName"> A <c>System.String</c> instance.</param>
		/// <param name="createdByDate"> A <c>System.DateTime?</c> instance.</param>
			/// <param name="logId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void PharmaLog_Insert(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid, System.Byte? fupcode, System.String aeuid, System.Int16? logTypeId, System.String logDetail, System.Int16? createdById, System.String createdByName, System.DateTime? createdByDate, ref System.Int32? logId);
		
		#endregion
		
		#region bbDelegationLogLatestRowsActiveOnly_getByCentreID 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRowsActiveOnly_getByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRowsActiveOnly_getByCentreID(System.Int32? centreId)
		{
			return DelegationLogLatestRowsActiveOnly_getByCentreID(null, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRowsActiveOnly_getByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRowsActiveOnly_getByCentreID(int start, int pageLength, System.Int32? centreId)
		{
			return DelegationLogLatestRowsActiveOnly_getByCentreID(null, start, pageLength , centreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRowsActiveOnly_getByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRowsActiveOnly_getByCentreID(TransactionManager transactionManager, System.Int32? centreId)
		{
			return DelegationLogLatestRowsActiveOnly_getByCentreID(transactionManager, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRowsActiveOnly_getByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLogLatestRowsActiveOnly_getByCentreID(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreId);
		
		#endregion
		
		#region bbQuery_GetPaged_ValidPatientsOnly 
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetPaged_ValidPatientsOnly' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetPaged_ValidPatientsOnly(System.String whereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize)
		{
			return Query_GetPaged_ValidPatientsOnly(null, 0, int.MaxValue , whereClause, orderBy, pageIndex, pageSize);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetPaged_ValidPatientsOnly' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetPaged_ValidPatientsOnly(int start, int pageLength, System.String whereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize)
		{
			return Query_GetPaged_ValidPatientsOnly(null, start, pageLength , whereClause, orderBy, pageIndex, pageSize);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_GetPaged_ValidPatientsOnly' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetPaged_ValidPatientsOnly(TransactionManager transactionManager, System.String whereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize)
		{
			return Query_GetPaged_ValidPatientsOnly(transactionManager, 0, int.MaxValue , whereClause, orderBy, pageIndex, pageSize);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetPaged_ValidPatientsOnly' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_GetPaged_ValidPatientsOnly(TransactionManager transactionManager, int start, int pageLength , System.String whereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize);
		
		#endregion
		
		#region bbAdverseEventRpt24_GetByFupaeid_ReportSuggestions 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_GetByFupaeid_ReportSuggestions' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="suggestionCount"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt24_GetByFupaeid_ReportSuggestions(System.Int32? fupaeid, ref System.Int32? suggestionCount)
		{
			return AdverseEventRpt24_GetByFupaeid_ReportSuggestions(null, 0, int.MaxValue , fupaeid, ref suggestionCount);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_GetByFupaeid_ReportSuggestions' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="suggestionCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt24_GetByFupaeid_ReportSuggestions(int start, int pageLength, System.Int32? fupaeid, ref System.Int32? suggestionCount)
		{
			return AdverseEventRpt24_GetByFupaeid_ReportSuggestions(null, start, pageLength , fupaeid, ref suggestionCount);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_GetByFupaeid_ReportSuggestions' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="suggestionCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt24_GetByFupaeid_ReportSuggestions(TransactionManager transactionManager, System.Int32? fupaeid, ref System.Int32? suggestionCount)
		{
			return AdverseEventRpt24_GetByFupaeid_ReportSuggestions(transactionManager, 0, int.MaxValue , fupaeid, ref suggestionCount);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_GetByFupaeid_ReportSuggestions' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="suggestionCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventRpt24_GetByFupaeid_ReportSuggestions(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid, ref System.Int32? suggestionCount);
		
		#endregion
		
		#region bbDelegationLogApprovals_GetByCentreID 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogApprovals_GetByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogApprovals_GetByCentreID(System.Int32? centreId)
		{
			return DelegationLogApprovals_GetByCentreID(null, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogApprovals_GetByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogApprovals_GetByCentreID(int start, int pageLength, System.Int32? centreId)
		{
			return DelegationLogApprovals_GetByCentreID(null, start, pageLength , centreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLogApprovals_GetByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogApprovals_GetByCentreID(TransactionManager transactionManager, System.Int32? centreId)
		{
			return DelegationLogApprovals_GetByCentreID(transactionManager, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogApprovals_GetByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLogApprovals_GetByCentreID(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreId);
		
		#endregion
		
		#region bbLoginLog_getLastTenLoginsByUser 
		
		/// <summary>
		///	This method wrap the 'bbLoginLog_getLastTenLoginsByUser' stored procedure. 
		/// </summary>
		/// <param name="badbiruserid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet LoginLog_getLastTenLoginsByUser(System.Int32? badbiruserid)
		{
			return LoginLog_getLastTenLoginsByUser(null, 0, int.MaxValue , badbiruserid);
		}
		
		/// <summary>
		///	This method wrap the 'bbLoginLog_getLastTenLoginsByUser' stored procedure. 
		/// </summary>
		/// <param name="badbiruserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet LoginLog_getLastTenLoginsByUser(int start, int pageLength, System.Int32? badbiruserid)
		{
			return LoginLog_getLastTenLoginsByUser(null, start, pageLength , badbiruserid);
		}
				
		/// <summary>
		///	This method wrap the 'bbLoginLog_getLastTenLoginsByUser' stored procedure. 
		/// </summary>
		/// <param name="badbiruserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet LoginLog_getLastTenLoginsByUser(TransactionManager transactionManager, System.Int32? badbiruserid)
		{
			return LoginLog_getLastTenLoginsByUser(transactionManager, 0, int.MaxValue , badbiruserid);
		}
		
		/// <summary>
		///	This method wrap the 'bbLoginLog_getLastTenLoginsByUser' stored procedure. 
		/// </summary>
		/// <param name="badbiruserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet LoginLog_getLastTenLoginsByUser(TransactionManager transactionManager, int start, int pageLength , System.Int32? badbiruserid);
		
		#endregion
		
		#region bbQueryTypelkp_GetAllForAdmin 
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForAdmin' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForAdmin()
		{
			return QueryTypelkp_GetAllForAdmin(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForAdmin' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForAdmin(int start, int pageLength)
		{
			return QueryTypelkp_GetAllForAdmin(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForAdmin' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForAdmin(TransactionManager transactionManager)
		{
			return QueryTypelkp_GetAllForAdmin(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForAdmin' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryTypelkp_GetAllForAdmin(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbCentreReportOnDemand_GetByCentreid 
		
		/// <summary>
		///	This method wrap the 'bbCentreReportOnDemand_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="rptStart"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="rptEnd"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreReportOnDemand_GetByCentreid(System.Int32? centreid, System.DateTime? rptStart, System.DateTime? rptEnd)
		{
			return CentreReportOnDemand_GetByCentreid(null, 0, int.MaxValue , centreid, rptStart, rptEnd);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentreReportOnDemand_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="rptStart"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="rptEnd"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreReportOnDemand_GetByCentreid(int start, int pageLength, System.Int32? centreid, System.DateTime? rptStart, System.DateTime? rptEnd)
		{
			return CentreReportOnDemand_GetByCentreid(null, start, pageLength , centreid, rptStart, rptEnd);
		}
				
		/// <summary>
		///	This method wrap the 'bbCentreReportOnDemand_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="rptStart"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="rptEnd"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreReportOnDemand_GetByCentreid(TransactionManager transactionManager, System.Int32? centreid, System.DateTime? rptStart, System.DateTime? rptEnd)
		{
			return CentreReportOnDemand_GetByCentreid(transactionManager, 0, int.MaxValue , centreid, rptStart, rptEnd);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentreReportOnDemand_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="rptStart"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="rptEnd"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet CentreReportOnDemand_GetByCentreid(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid, System.DateTime? rptStart, System.DateTime? rptEnd);
		
		#endregion
		
		#region bbGenerateFUPSet 
		
		/// <summary>
		///	This method wrap the 'bbGenerateFUPSet' stored procedure. 
		/// </summary>
		/// <param name="cohorthistoryid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="duedate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="createdbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="createdbyname"> A <c>System.String</c> instance.</param>
		/// <param name="createddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void GenerateFUPSet(System.Int32? cohorthistoryid, System.Int32? centreid, System.DateTime? duedate, System.Int32? createdbyid, System.String createdbyname, System.DateTime? createddate, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			 GenerateFUPSet(null, 0, int.MaxValue , cohorthistoryid, centreid, duedate, createdbyid, createdbyname, createddate, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
		
		/// <summary>
		///	This method wrap the 'bbGenerateFUPSet' stored procedure. 
		/// </summary>
		/// <param name="cohorthistoryid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="duedate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="createdbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="createdbyname"> A <c>System.String</c> instance.</param>
		/// <param name="createddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void GenerateFUPSet(int start, int pageLength, System.Int32? cohorthistoryid, System.Int32? centreid, System.DateTime? duedate, System.Int32? createdbyid, System.String createdbyname, System.DateTime? createddate, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			 GenerateFUPSet(null, start, pageLength , cohorthistoryid, centreid, duedate, createdbyid, createdbyname, createddate, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
				
		/// <summary>
		///	This method wrap the 'bbGenerateFUPSet' stored procedure. 
		/// </summary>
		/// <param name="cohorthistoryid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="duedate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="createdbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="createdbyname"> A <c>System.String</c> instance.</param>
		/// <param name="createddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void GenerateFUPSet(TransactionManager transactionManager, System.Int32? cohorthistoryid, System.Int32? centreid, System.DateTime? duedate, System.Int32? createdbyid, System.String createdbyname, System.DateTime? createddate, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			 GenerateFUPSet(transactionManager, 0, int.MaxValue , cohorthistoryid, centreid, duedate, createdbyid, createdbyname, createddate, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
		
		/// <summary>
		///	This method wrap the 'bbGenerateFUPSet' stored procedure. 
		/// </summary>
		/// <param name="cohorthistoryid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="duedate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="createdbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="createdbyname"> A <c>System.String</c> instance.</param>
		/// <param name="createddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void GenerateFUPSet(TransactionManager transactionManager, int start, int pageLength , System.Int32? cohorthistoryid, System.Int32? centreid, System.DateTime? duedate, System.Int32? createdbyid, System.String createdbyname, System.DateTime? createddate, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate);
		
		#endregion
		
		#region bbAdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID(System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID(null, 0, int.MaxValue , aeLinkageRowId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID(int start, int pageLength, System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID(null, start, pageLength , aeLinkageRowId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID(TransactionManager transactionManager, System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID(transactionManager, 0, int.MaxValue , aeLinkageRowId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventLinkageProcessing_GetAllInstancesByAELinkageRowID(TransactionManager transactionManager, int start, int pageLength , System.Int32? aeLinkageRowId);
		
		#endregion
		
		#region bbAdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE()
		{
			 AdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE(int start, int pageLength)
		{
			 AdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE(TransactionManager transactionManager)
		{
			 AdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void AdverseEventRpt24_ImportManchesterCodesFromOldSystem___DELETE(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbPharmaLog_GetByCreatedByID 
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByCreatedByID' stored procedure. 
		/// </summary>
		/// <param name="createdById"> A <c>System.Int16?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByCreatedByID(System.Int16? createdById)
		{
			return PharmaLog_GetByCreatedByID(null, 0, int.MaxValue , createdById);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByCreatedByID' stored procedure. 
		/// </summary>
		/// <param name="createdById"> A <c>System.Int16?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByCreatedByID(int start, int pageLength, System.Int16? createdById)
		{
			return PharmaLog_GetByCreatedByID(null, start, pageLength , createdById);
		}
				
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByCreatedByID' stored procedure. 
		/// </summary>
		/// <param name="createdById"> A <c>System.Int16?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByCreatedByID(TransactionManager transactionManager, System.Int16? createdById)
		{
			return PharmaLog_GetByCreatedByID(transactionManager, 0, int.MaxValue , createdById);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByCreatedByID' stored procedure. 
		/// </summary>
		/// <param name="createdById"> A <c>System.Int16?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PharmaLog_GetByCreatedByID(TransactionManager transactionManager, int start, int pageLength , System.Int16? createdById);
		
		#endregion
		
		#region bbUpdateCleanedFlag 
		
		/// <summary>
		///	This method wrap the 'bbUpdateCleanedFlag' stored procedure. 
		/// </summary>
		/// <param name="aeuidmed"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateCleanedFlag(System.String aeuidmed, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			 UpdateCleanedFlag(null, 0, int.MaxValue , aeuidmed, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
		
		/// <summary>
		///	This method wrap the 'bbUpdateCleanedFlag' stored procedure. 
		/// </summary>
		/// <param name="aeuidmed"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateCleanedFlag(int start, int pageLength, System.String aeuidmed, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			 UpdateCleanedFlag(null, start, pageLength , aeuidmed, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
				
		/// <summary>
		///	This method wrap the 'bbUpdateCleanedFlag' stored procedure. 
		/// </summary>
		/// <param name="aeuidmed"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void UpdateCleanedFlag(TransactionManager transactionManager, System.String aeuidmed, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			 UpdateCleanedFlag(transactionManager, 0, int.MaxValue , aeuidmed, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
		
		/// <summary>
		///	This method wrap the 'bbUpdateCleanedFlag' stored procedure. 
		/// </summary>
		/// <param name="aeuidmed"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void UpdateCleanedFlag(TransactionManager transactionManager, int start, int pageLength , System.String aeuidmed, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate);
		
		#endregion
		
		#region bbAdverseEventFup_CustomSearch 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_CustomSearch' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="meddraWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="mcrWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="rptWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="esiWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="patientTablesWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="addInfoWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="maxResults"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_CustomSearch(System.String whereClause, System.String meddraWhereClause, System.String mcrWhereClause, System.String rptWhereClause, System.String esiWhereClause, System.String patientTablesWhereClause, System.String addInfoWhereClause, System.String orderBy, System.Int32? maxResults)
		{
			return AdverseEventFup_CustomSearch(null, 0, int.MaxValue , whereClause, meddraWhereClause, mcrWhereClause, rptWhereClause, esiWhereClause, patientTablesWhereClause, addInfoWhereClause, orderBy, maxResults);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_CustomSearch' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="meddraWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="mcrWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="rptWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="esiWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="patientTablesWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="addInfoWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="maxResults"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_CustomSearch(int start, int pageLength, System.String whereClause, System.String meddraWhereClause, System.String mcrWhereClause, System.String rptWhereClause, System.String esiWhereClause, System.String patientTablesWhereClause, System.String addInfoWhereClause, System.String orderBy, System.Int32? maxResults)
		{
			return AdverseEventFup_CustomSearch(null, start, pageLength , whereClause, meddraWhereClause, mcrWhereClause, rptWhereClause, esiWhereClause, patientTablesWhereClause, addInfoWhereClause, orderBy, maxResults);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_CustomSearch' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="meddraWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="mcrWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="rptWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="esiWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="patientTablesWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="addInfoWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="maxResults"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_CustomSearch(TransactionManager transactionManager, System.String whereClause, System.String meddraWhereClause, System.String mcrWhereClause, System.String rptWhereClause, System.String esiWhereClause, System.String patientTablesWhereClause, System.String addInfoWhereClause, System.String orderBy, System.Int32? maxResults)
		{
			return AdverseEventFup_CustomSearch(transactionManager, 0, int.MaxValue , whereClause, meddraWhereClause, mcrWhereClause, rptWhereClause, esiWhereClause, patientTablesWhereClause, addInfoWhereClause, orderBy, maxResults);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_CustomSearch' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="meddraWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="mcrWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="rptWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="esiWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="patientTablesWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="addInfoWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="maxResults"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_CustomSearch(TransactionManager transactionManager, int start, int pageLength , System.String whereClause, System.String meddraWhereClause, System.String mcrWhereClause, System.String rptWhereClause, System.String esiWhereClause, System.String patientTablesWhereClause, System.String addInfoWhereClause, System.String orderBy, System.Int32? maxResults);
		
		#endregion
		
		#region bbAdverseEventAll_getSeriousByPatientid 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventAll_getSeriousByPatientid' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventAll_getSeriousByPatientid(System.Int32? patientid)
		{
			return AdverseEventAll_getSeriousByPatientid(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventAll_getSeriousByPatientid' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventAll_getSeriousByPatientid(int start, int pageLength, System.Int32? patientid)
		{
			return AdverseEventAll_getSeriousByPatientid(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventAll_getSeriousByPatientid' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventAll_getSeriousByPatientid(TransactionManager transactionManager, System.Int32? patientid)
		{
			return AdverseEventAll_getSeriousByPatientid(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventAll_getSeriousByPatientid' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventAll_getSeriousByPatientid(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbPatientCohortTracking_Get_Activity_List_Clinician 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_Get_Activity_List_Clinician' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateto"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_Get_Activity_List_Clinician(System.Int32? centreid, System.DateTime? datefrom, System.DateTime? dateto, System.Int32? isTraining)
		{
			return PatientCohortTracking_Get_Activity_List_Clinician(null, 0, int.MaxValue , centreid, datefrom, dateto, isTraining);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_Get_Activity_List_Clinician' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateto"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_Get_Activity_List_Clinician(int start, int pageLength, System.Int32? centreid, System.DateTime? datefrom, System.DateTime? dateto, System.Int32? isTraining)
		{
			return PatientCohortTracking_Get_Activity_List_Clinician(null, start, pageLength , centreid, datefrom, dateto, isTraining);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_Get_Activity_List_Clinician' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateto"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_Get_Activity_List_Clinician(TransactionManager transactionManager, System.Int32? centreid, System.DateTime? datefrom, System.DateTime? dateto, System.Int32? isTraining)
		{
			return PatientCohortTracking_Get_Activity_List_Clinician(transactionManager, 0, int.MaxValue , centreid, datefrom, dateto, isTraining);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_Get_Activity_List_Clinician' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateto"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortTracking_Get_Activity_List_Clinician(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid, System.DateTime? datefrom, System.DateTime? dateto, System.Int32? isTraining);
		
		#endregion
		
		#region bbDelegationLogLatestRows_getByCentreIDUserID 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRows_getByCentreIDUserID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRows_getByCentreIDUserID(System.Int32? centreId, System.Int32? badbirUserId)
		{
			return DelegationLogLatestRows_getByCentreIDUserID(null, 0, int.MaxValue , centreId, badbirUserId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRows_getByCentreIDUserID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRows_getByCentreIDUserID(int start, int pageLength, System.Int32? centreId, System.Int32? badbirUserId)
		{
			return DelegationLogLatestRows_getByCentreIDUserID(null, start, pageLength , centreId, badbirUserId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRows_getByCentreIDUserID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRows_getByCentreIDUserID(TransactionManager transactionManager, System.Int32? centreId, System.Int32? badbirUserId)
		{
			return DelegationLogLatestRows_getByCentreIDUserID(transactionManager, 0, int.MaxValue , centreId, badbirUserId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRows_getByCentreIDUserID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLogLatestRows_getByCentreIDUserID(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreId, System.Int32? badbirUserId);
		
		#endregion
		
		#region bbQuery_MarkAsClinicianUnread 
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsClinicianUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsClinicianUnread(System.Int32? qid)
		{
			return Query_MarkAsClinicianUnread(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsClinicianUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsClinicianUnread(int start, int pageLength, System.Int32? qid)
		{
			return Query_MarkAsClinicianUnread(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsClinicianUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsClinicianUnread(TransactionManager transactionManager, System.Int32? qid)
		{
			return Query_MarkAsClinicianUnread(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsClinicianUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_MarkAsClinicianUnread(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbPatient_Get_NHSICSent_Withdrawn_DeletionExtract 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_NHSICSent_Withdrawn_DeletionExtract' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_NHSICSent_Withdrawn_DeletionExtract()
		{
			return Patient_Get_NHSICSent_Withdrawn_DeletionExtract(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_NHSICSent_Withdrawn_DeletionExtract' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_NHSICSent_Withdrawn_DeletionExtract(int start, int pageLength)
		{
			return Patient_Get_NHSICSent_Withdrawn_DeletionExtract(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Get_NHSICSent_Withdrawn_DeletionExtract' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_NHSICSent_Withdrawn_DeletionExtract(TransactionManager transactionManager)
		{
			return Patient_Get_NHSICSent_Withdrawn_DeletionExtract(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_NHSICSent_Withdrawn_DeletionExtract' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_Get_NHSICSent_Withdrawn_DeletionExtract(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbAdverseEventFup_GetPagedCustom 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetPagedCustom' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="meddraWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="mcrWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="rptWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="esiWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="patientTablesWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="addInfoWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
			/// <param name="recordCount"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetPagedCustom(System.String whereClause, System.String meddraWhereClause, System.String mcrWhereClause, System.String rptWhereClause, System.String esiWhereClause, System.String patientTablesWhereClause, System.String addInfoWhereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize, ref System.Int32? recordCount)
		{
			return AdverseEventFup_GetPagedCustom(null, 0, int.MaxValue , whereClause, meddraWhereClause, mcrWhereClause, rptWhereClause, esiWhereClause, patientTablesWhereClause, addInfoWhereClause, orderBy, pageIndex, pageSize, ref recordCount);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetPagedCustom' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="meddraWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="mcrWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="rptWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="esiWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="patientTablesWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="addInfoWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
			/// <param name="recordCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetPagedCustom(int start, int pageLength, System.String whereClause, System.String meddraWhereClause, System.String mcrWhereClause, System.String rptWhereClause, System.String esiWhereClause, System.String patientTablesWhereClause, System.String addInfoWhereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize, ref System.Int32? recordCount)
		{
			return AdverseEventFup_GetPagedCustom(null, start, pageLength , whereClause, meddraWhereClause, mcrWhereClause, rptWhereClause, esiWhereClause, patientTablesWhereClause, addInfoWhereClause, orderBy, pageIndex, pageSize, ref recordCount);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetPagedCustom' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="meddraWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="mcrWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="rptWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="esiWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="patientTablesWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="addInfoWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
			/// <param name="recordCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetPagedCustom(TransactionManager transactionManager, System.String whereClause, System.String meddraWhereClause, System.String mcrWhereClause, System.String rptWhereClause, System.String esiWhereClause, System.String patientTablesWhereClause, System.String addInfoWhereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize, ref System.Int32? recordCount)
		{
			return AdverseEventFup_GetPagedCustom(transactionManager, 0, int.MaxValue , whereClause, meddraWhereClause, mcrWhereClause, rptWhereClause, esiWhereClause, patientTablesWhereClause, addInfoWhereClause, orderBy, pageIndex, pageSize, ref recordCount);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetPagedCustom' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="meddraWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="mcrWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="rptWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="esiWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="patientTablesWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="addInfoWhereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
			/// <param name="recordCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_GetPagedCustom(TransactionManager transactionManager, int start, int pageLength , System.String whereClause, System.String meddraWhereClause, System.String mcrWhereClause, System.String rptWhereClause, System.String esiWhereClause, System.String patientTablesWhereClause, System.String addInfoWhereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize, ref System.Int32? recordCount);
		
		#endregion
		
		#region bbQueryTypelkp_GetAllForCentre 
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForCentre' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForCentre()
		{
			return QueryTypelkp_GetAllForCentre(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForCentre' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForCentre(int start, int pageLength)
		{
			return QueryTypelkp_GetAllForCentre(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForCentre' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForCentre(TransactionManager transactionManager)
		{
			return QueryTypelkp_GetAllForCentre(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForCentre' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryTypelkp_GetAllForCentre(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbQuery_GetByChid_AllFeedback 
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_AllFeedback' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_AllFeedback(System.Int32? chid)
		{
			return Query_GetByChid_AllFeedback(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_AllFeedback' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_AllFeedback(int start, int pageLength, System.Int32? chid)
		{
			return Query_GetByChid_AllFeedback(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_AllFeedback' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_AllFeedback(TransactionManager transactionManager, System.Int32? chid)
		{
			return Query_GetByChid_AllFeedback(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_AllFeedback' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_GetByChid_AllFeedback(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbESIlkp_Get_List_Valid 
		
		/// <summary>
		///	This method wrap the 'bbESIlkp_Get_List_Valid' stored procedure. 
		/// </summary>
		/// <param name="aeEsIid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet ESIlkp_Get_List_Valid(System.Int32? aeEsIid)
		{
			return ESIlkp_Get_List_Valid(null, 0, int.MaxValue , aeEsIid);
		}
		
		/// <summary>
		///	This method wrap the 'bbESIlkp_Get_List_Valid' stored procedure. 
		/// </summary>
		/// <param name="aeEsIid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet ESIlkp_Get_List_Valid(int start, int pageLength, System.Int32? aeEsIid)
		{
			return ESIlkp_Get_List_Valid(null, start, pageLength , aeEsIid);
		}
				
		/// <summary>
		///	This method wrap the 'bbESIlkp_Get_List_Valid' stored procedure. 
		/// </summary>
		/// <param name="aeEsIid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet ESIlkp_Get_List_Valid(TransactionManager transactionManager, System.Int32? aeEsIid)
		{
			return ESIlkp_Get_List_Valid(transactionManager, 0, int.MaxValue , aeEsIid);
		}
		
		/// <summary>
		///	This method wrap the 'bbESIlkp_Get_List_Valid' stored procedure. 
		/// </summary>
		/// <param name="aeEsIid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet ESIlkp_Get_List_Valid(TransactionManager transactionManager, int start, int pageLength , System.Int32? aeEsIid);
		
		#endregion
		
		#region bbPatientCohortHistory_GetMostRecentbyPatientId 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortHistory_GetMostRecentbyPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortHistory_GetMostRecentbyPatientId(System.Int32? patientid)
		{
			return PatientCohortHistory_GetMostRecentbyPatientId(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortHistory_GetMostRecentbyPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortHistory_GetMostRecentbyPatientId(int start, int pageLength, System.Int32? patientid)
		{
			return PatientCohortHistory_GetMostRecentbyPatientId(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortHistory_GetMostRecentbyPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortHistory_GetMostRecentbyPatientId(TransactionManager transactionManager, System.Int32? patientid)
		{
			return PatientCohortHistory_GetMostRecentbyPatientId(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortHistory_GetMostRecentbyPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortHistory_GetMostRecentbyPatientId(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbPatient_Check_Access_forClinician_forFUP 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Check_Access_forClinician_forFUP' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiRuserid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="clinicianHasAccessToFup"> A <c>System.Boolean?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Patient_Check_Access_forClinician_forFUP(System.Int32? fupid, System.Int32? badbiRuserid, ref System.Boolean? clinicianHasAccessToFup)
		{
			 Patient_Check_Access_forClinician_forFUP(null, 0, int.MaxValue , fupid, badbiRuserid, ref clinicianHasAccessToFup);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Check_Access_forClinician_forFUP' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiRuserid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="clinicianHasAccessToFup"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Patient_Check_Access_forClinician_forFUP(int start, int pageLength, System.Int32? fupid, System.Int32? badbiRuserid, ref System.Boolean? clinicianHasAccessToFup)
		{
			 Patient_Check_Access_forClinician_forFUP(null, start, pageLength , fupid, badbiRuserid, ref clinicianHasAccessToFup);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Check_Access_forClinician_forFUP' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiRuserid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="clinicianHasAccessToFup"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Patient_Check_Access_forClinician_forFUP(TransactionManager transactionManager, System.Int32? fupid, System.Int32? badbiRuserid, ref System.Boolean? clinicianHasAccessToFup)
		{
			 Patient_Check_Access_forClinician_forFUP(transactionManager, 0, int.MaxValue , fupid, badbiRuserid, ref clinicianHasAccessToFup);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Check_Access_forClinician_forFUP' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiRuserid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="clinicianHasAccessToFup"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void Patient_Check_Access_forClinician_forFUP(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupid, System.Int32? badbiRuserid, ref System.Boolean? clinicianHasAccessToFup);
		
		#endregion
		
		#region bbAEEsi_NotFilledCount 
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledCount' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledCount(System.Int32? patientid)
		{
			return AEEsi_NotFilledCount(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledCount' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledCount(int start, int pageLength, System.Int32? patientid)
		{
			return AEEsi_NotFilledCount(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledCount' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledCount(TransactionManager transactionManager, System.Int32? patientid)
		{
			return AEEsi_NotFilledCount(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledCount' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AEEsi_NotFilledCount(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbPatientDrug_AdminGetDrugSummaryForEdit 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminGetDrugSummaryForEdit' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_AdminGetDrugSummaryForEdit(System.Int32? patientid)
		{
			return PatientDrug_AdminGetDrugSummaryForEdit(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminGetDrugSummaryForEdit' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_AdminGetDrugSummaryForEdit(int start, int pageLength, System.Int32? patientid)
		{
			return PatientDrug_AdminGetDrugSummaryForEdit(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminGetDrugSummaryForEdit' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_AdminGetDrugSummaryForEdit(TransactionManager transactionManager, System.Int32? patientid)
		{
			return PatientDrug_AdminGetDrugSummaryForEdit(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminGetDrugSummaryForEdit' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientDrug_AdminGetDrugSummaryForEdit(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbAdverseEventFup_GetActiveByFupId_OtherSources 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId_OtherSources' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId_OtherSources(System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId_OtherSources(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId_OtherSources' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId_OtherSources(int start, int pageLength, System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId_OtherSources(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId_OtherSources' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId_OtherSources(TransactionManager transactionManager, System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId_OtherSources(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId_OtherSources' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_GetActiveByFupId_OtherSources(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbQueryForCentre_MarkAsClinicianRead 
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsClinicianRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsClinicianRead(System.Int32? qid)
		{
			return QueryForCentre_MarkAsClinicianRead(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsClinicianRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsClinicianRead(int start, int pageLength, System.Int32? qid)
		{
			return QueryForCentre_MarkAsClinicianRead(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsClinicianRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsClinicianRead(TransactionManager transactionManager, System.Int32? qid)
		{
			return QueryForCentre_MarkAsClinicianRead(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsClinicianRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryForCentre_MarkAsClinicianRead(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbGetNewsletterMailingList 
		
		/// <summary>
		///	This method wrap the 'bbGetNewsletterMailingList' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetNewsletterMailingList()
		{
			return GetNewsletterMailingList(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbGetNewsletterMailingList' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetNewsletterMailingList(int start, int pageLength)
		{
			return GetNewsletterMailingList(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbGetNewsletterMailingList' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetNewsletterMailingList(TransactionManager transactionManager)
		{
			return GetNewsletterMailingList(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbGetNewsletterMailingList' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet GetNewsletterMailingList(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbQueryForCentre_MarkQuerySolved 
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkQuerySolved' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkQuerySolved(System.Int32? qid)
		{
			return QueryForCentre_MarkQuerySolved(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkQuerySolved' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkQuerySolved(int start, int pageLength, System.Int32? qid)
		{
			return QueryForCentre_MarkQuerySolved(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkQuerySolved' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkQuerySolved(TransactionManager transactionManager, System.Int32? qid)
		{
			return QueryForCentre_MarkQuerySolved(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkQuerySolved' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryForCentre_MarkQuerySolved(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbPatient_Get_Report_Suggestions 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_Report_Suggestions' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_Report_Suggestions(System.Int32? fupaeid)
		{
			return Patient_Get_Report_Suggestions(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_Report_Suggestions' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_Report_Suggestions(int start, int pageLength, System.Int32? fupaeid)
		{
			return Patient_Get_Report_Suggestions(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Get_Report_Suggestions' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_Report_Suggestions(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return Patient_Get_Report_Suggestions(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_Report_Suggestions' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_Get_Report_Suggestions(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbPatientCohortTracking_Get_Activity_List 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_Get_Activity_List' stored procedure. 
		/// </summary>
		/// <param name="onlybaseline"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="onlyFuPs"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupstatus"> A <c>System.Int32?</c> instance.</param>
		/// <param name="patientstatus"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateto"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_Get_Activity_List(System.Boolean? onlybaseline, System.Boolean? onlyFuPs, System.Int32? centreid, System.Int32? fupstatus, System.Int32? patientstatus, System.DateTime? datefrom, System.DateTime? dateto, System.Int32? isTraining)
		{
			return PatientCohortTracking_Get_Activity_List(null, 0, int.MaxValue , onlybaseline, onlyFuPs, centreid, fupstatus, patientstatus, datefrom, dateto, isTraining);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_Get_Activity_List' stored procedure. 
		/// </summary>
		/// <param name="onlybaseline"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="onlyFuPs"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupstatus"> A <c>System.Int32?</c> instance.</param>
		/// <param name="patientstatus"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateto"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_Get_Activity_List(int start, int pageLength, System.Boolean? onlybaseline, System.Boolean? onlyFuPs, System.Int32? centreid, System.Int32? fupstatus, System.Int32? patientstatus, System.DateTime? datefrom, System.DateTime? dateto, System.Int32? isTraining)
		{
			return PatientCohortTracking_Get_Activity_List(null, start, pageLength , onlybaseline, onlyFuPs, centreid, fupstatus, patientstatus, datefrom, dateto, isTraining);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_Get_Activity_List' stored procedure. 
		/// </summary>
		/// <param name="onlybaseline"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="onlyFuPs"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupstatus"> A <c>System.Int32?</c> instance.</param>
		/// <param name="patientstatus"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateto"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_Get_Activity_List(TransactionManager transactionManager, System.Boolean? onlybaseline, System.Boolean? onlyFuPs, System.Int32? centreid, System.Int32? fupstatus, System.Int32? patientstatus, System.DateTime? datefrom, System.DateTime? dateto, System.Int32? isTraining)
		{
			return PatientCohortTracking_Get_Activity_List(transactionManager, 0, int.MaxValue , onlybaseline, onlyFuPs, centreid, fupstatus, patientstatus, datefrom, dateto, isTraining);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_Get_Activity_List' stored procedure. 
		/// </summary>
		/// <param name="onlybaseline"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="onlyFuPs"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupstatus"> A <c>System.Int32?</c> instance.</param>
		/// <param name="patientstatus"> A <c>System.Int32?</c> instance.</param>
		/// <param name="datefrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateto"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortTracking_Get_Activity_List(TransactionManager transactionManager, int start, int pageLength , System.Boolean? onlybaseline, System.Boolean? onlyFuPs, System.Int32? centreid, System.Int32? fupstatus, System.Int32? patientstatus, System.DateTime? datefrom, System.DateTime? dateto, System.Int32? isTraining);
		
		#endregion
		
		#region bbAdverseEventFup_GetActiveByFupId 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId(System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId(int start, int pageLength, System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId(TransactionManager transactionManager, System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_GetActiveByFupId(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbPharmaLog_GetByDateRange 
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByDateRange' stored procedure. 
		/// </summary>
		/// <param name="startCreatedByDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="stopCreatedByDate"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByDateRange(System.DateTime? startCreatedByDate, System.DateTime? stopCreatedByDate)
		{
			return PharmaLog_GetByDateRange(null, 0, int.MaxValue , startCreatedByDate, stopCreatedByDate);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByDateRange' stored procedure. 
		/// </summary>
		/// <param name="startCreatedByDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="stopCreatedByDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByDateRange(int start, int pageLength, System.DateTime? startCreatedByDate, System.DateTime? stopCreatedByDate)
		{
			return PharmaLog_GetByDateRange(null, start, pageLength , startCreatedByDate, stopCreatedByDate);
		}
				
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByDateRange' stored procedure. 
		/// </summary>
		/// <param name="startCreatedByDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="stopCreatedByDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByDateRange(TransactionManager transactionManager, System.DateTime? startCreatedByDate, System.DateTime? stopCreatedByDate)
		{
			return PharmaLog_GetByDateRange(transactionManager, 0, int.MaxValue , startCreatedByDate, stopCreatedByDate);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByDateRange' stored procedure. 
		/// </summary>
		/// <param name="startCreatedByDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="stopCreatedByDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PharmaLog_GetByDateRange(TransactionManager transactionManager, int start, int pageLength , System.DateTime? startCreatedByDate, System.DateTime? stopCreatedByDate);
		
		#endregion
		
		#region bbPharmaLog_GetByChidFupno 
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByChidFupno' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupcode"> A <c>System.Byte?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByChidFupno(System.Int32? chid, System.Byte? fupcode)
		{
			return PharmaLog_GetByChidFupno(null, 0, int.MaxValue , chid, fupcode);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByChidFupno' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupcode"> A <c>System.Byte?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByChidFupno(int start, int pageLength, System.Int32? chid, System.Byte? fupcode)
		{
			return PharmaLog_GetByChidFupno(null, start, pageLength , chid, fupcode);
		}
				
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByChidFupno' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupcode"> A <c>System.Byte?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByChidFupno(TransactionManager transactionManager, System.Int32? chid, System.Byte? fupcode)
		{
			return PharmaLog_GetByChidFupno(transactionManager, 0, int.MaxValue , chid, fupcode);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByChidFupno' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupcode"> A <c>System.Byte?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PharmaLog_GetByChidFupno(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid, System.Byte? fupcode);
		
		#endregion
		
		#region bbQueryForCentre_MarkAsAdminUnread 
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsAdminUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsAdminUnread(System.Int32? qid)
		{
			return QueryForCentre_MarkAsAdminUnread(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsAdminUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsAdminUnread(int start, int pageLength, System.Int32? qid)
		{
			return QueryForCentre_MarkAsAdminUnread(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsAdminUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsAdminUnread(TransactionManager transactionManager, System.Int32? qid)
		{
			return QueryForCentre_MarkAsAdminUnread(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsAdminUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryForCentre_MarkAsAdminUnread(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbQuery_MarkQuerySolved 
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkQuerySolved' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkQuerySolved(System.Int32? qid)
		{
			return Query_MarkQuerySolved(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkQuerySolved' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkQuerySolved(int start, int pageLength, System.Int32? qid)
		{
			return Query_MarkQuerySolved(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_MarkQuerySolved' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkQuerySolved(TransactionManager transactionManager, System.Int32? qid)
		{
			return Query_MarkQuerySolved(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkQuerySolved' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_MarkQuerySolved(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbDrugLkp_Get_List_Previous_ByCohort 
		
		/// <summary>
		///	This method wrap the 'bbDrugLkp_Get_List_Previous_ByCohort' stored procedure. 
		/// </summary>
		/// <param name="cohortid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DrugLkp_Get_List_Previous_ByCohort(System.Int32? cohortid, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return DrugLkp_Get_List_Previous_ByCohort(null, 0, int.MaxValue , cohortid, patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDrugLkp_Get_List_Previous_ByCohort' stored procedure. 
		/// </summary>
		/// <param name="cohortid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DrugLkp_Get_List_Previous_ByCohort(int start, int pageLength, System.Int32? cohortid, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return DrugLkp_Get_List_Previous_ByCohort(null, start, pageLength , cohortid, patdrugid, isAdmin);
		}
				
		/// <summary>
		///	This method wrap the 'bbDrugLkp_Get_List_Previous_ByCohort' stored procedure. 
		/// </summary>
		/// <param name="cohortid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DrugLkp_Get_List_Previous_ByCohort(TransactionManager transactionManager, System.Int32? cohortid, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return DrugLkp_Get_List_Previous_ByCohort(transactionManager, 0, int.MaxValue , cohortid, patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDrugLkp_Get_List_Previous_ByCohort' stored procedure. 
		/// </summary>
		/// <param name="cohortid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DrugLkp_Get_List_Previous_ByCohort(TransactionManager transactionManager, int start, int pageLength , System.Int32? cohortid, System.Int32? patdrugid, System.Int32? isAdmin);
		
		#endregion
		
		#region bbAEEsi_NotFilledCountByCentreID 
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledCountByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledCountByCentreID(System.Int32? centreid)
		{
			return AEEsi_NotFilledCountByCentreID(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledCountByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledCountByCentreID(int start, int pageLength, System.Int32? centreid)
		{
			return AEEsi_NotFilledCountByCentreID(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledCountByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledCountByCentreID(TransactionManager transactionManager, System.Int32? centreid)
		{
			return AEEsi_NotFilledCountByCentreID(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledCountByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AEEsi_NotFilledCountByCentreID(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbDrugComorbiditylkp_GetSuggestionsByFupId 
		
		/// <summary>
		///	This method wrap the 'bbDrugComorbiditylkp_GetSuggestionsByFupId' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DrugComorbiditylkp_GetSuggestionsByFupId(System.Int32? fupid)
		{
			return DrugComorbiditylkp_GetSuggestionsByFupId(null, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbDrugComorbiditylkp_GetSuggestionsByFupId' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DrugComorbiditylkp_GetSuggestionsByFupId(int start, int pageLength, System.Int32? fupid)
		{
			return DrugComorbiditylkp_GetSuggestionsByFupId(null, start, pageLength , fupid);
		}
				
		/// <summary>
		///	This method wrap the 'bbDrugComorbiditylkp_GetSuggestionsByFupId' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DrugComorbiditylkp_GetSuggestionsByFupId(TransactionManager transactionManager, System.Int32? fupid)
		{
			return DrugComorbiditylkp_GetSuggestionsByFupId(transactionManager, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbDrugComorbiditylkp_GetSuggestionsByFupId' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DrugComorbiditylkp_GetSuggestionsByFupId(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupid);
		
		#endregion
		
		#region bbMonthwiseRecruitmentReportForCentre 
		
		/// <summary>
		///	This method wrap the 'bbMonthwiseRecruitmentReportForCentre' stored procedure. 
		/// </summary>
		/// <param name="inputCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet MonthwiseRecruitmentReportForCentre(System.Int32? inputCentreId)
		{
			return MonthwiseRecruitmentReportForCentre(null, 0, int.MaxValue , inputCentreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbMonthwiseRecruitmentReportForCentre' stored procedure. 
		/// </summary>
		/// <param name="inputCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet MonthwiseRecruitmentReportForCentre(int start, int pageLength, System.Int32? inputCentreId)
		{
			return MonthwiseRecruitmentReportForCentre(null, start, pageLength , inputCentreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbMonthwiseRecruitmentReportForCentre' stored procedure. 
		/// </summary>
		/// <param name="inputCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet MonthwiseRecruitmentReportForCentre(TransactionManager transactionManager, System.Int32? inputCentreId)
		{
			return MonthwiseRecruitmentReportForCentre(transactionManager, 0, int.MaxValue , inputCentreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbMonthwiseRecruitmentReportForCentre' stored procedure. 
		/// </summary>
		/// <param name="inputCentreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet MonthwiseRecruitmentReportForCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? inputCentreId);
		
		#endregion
		
		#region bbPharmaLog_GetWorkingDayStatus 
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetWorkingDayStatus' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetWorkingDayStatus()
		{
			return PharmaLog_GetWorkingDayStatus(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetWorkingDayStatus' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetWorkingDayStatus(int start, int pageLength)
		{
			return PharmaLog_GetWorkingDayStatus(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetWorkingDayStatus' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetWorkingDayStatus(TransactionManager transactionManager)
		{
			return PharmaLog_GetWorkingDayStatus(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetWorkingDayStatus' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PharmaLog_GetWorkingDayStatus(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbPatient_GetNextFupByPatientid 
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetNextFupByPatientid' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetNextFupByPatientid(System.Int32? patientid)
		{
			return Patient_GetNextFupByPatientid(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetNextFupByPatientid' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetNextFupByPatientid(int start, int pageLength, System.Int32? patientid)
		{
			return Patient_GetNextFupByPatientid(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_GetNextFupByPatientid' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_GetNextFupByPatientid(TransactionManager transactionManager, System.Int32? patientid)
		{
			return Patient_GetNextFupByPatientid(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_GetNextFupByPatientid' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_GetNextFupByPatientid(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbDruglkp_Get_List_Conventional_For_patdrugid 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid(System.Int32? patdrugid)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid(null, 0, int.MaxValue , patdrugid);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid(int start, int pageLength, System.Int32? patdrugid)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid(null, start, pageLength , patdrugid);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid(TransactionManager transactionManager, System.Int32? patdrugid)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid(transactionManager, 0, int.MaxValue , patdrugid);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Conventional_For_patdrugid(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid);
		
		#endregion
		
		#region bbPatientCohortTracking_ClearFupData 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_ClearFupData' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_ClearFupData(System.Int32? fupid)
		{
			return PatientCohortTracking_ClearFupData(null, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_ClearFupData' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_ClearFupData(int start, int pageLength, System.Int32? fupid)
		{
			return PatientCohortTracking_ClearFupData(null, start, pageLength , fupid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_ClearFupData' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_ClearFupData(TransactionManager transactionManager, System.Int32? fupid)
		{
			return PatientCohortTracking_ClearFupData(transactionManager, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_ClearFupData' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortTracking_ClearFupData(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupid);
		
		#endregion
		
		#region bbPatientDrug_GetPsoriasisTreatmentHistoryForAE 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetPsoriasisTreatmentHistoryForAE' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_GetPsoriasisTreatmentHistoryForAE(System.Int32? fupaeid)
		{
			return PatientDrug_GetPsoriasisTreatmentHistoryForAE(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetPsoriasisTreatmentHistoryForAE' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_GetPsoriasisTreatmentHistoryForAE(int start, int pageLength, System.Int32? fupaeid)
		{
			return PatientDrug_GetPsoriasisTreatmentHistoryForAE(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetPsoriasisTreatmentHistoryForAE' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_GetPsoriasisTreatmentHistoryForAE(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return PatientDrug_GetPsoriasisTreatmentHistoryForAE(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetPsoriasisTreatmentHistoryForAE' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientDrug_GetPsoriasisTreatmentHistoryForAE(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbQuery_GetByChid_ExcludeSolvedAndExcluded 
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_ExcludeSolvedAndExcluded' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_ExcludeSolvedAndExcluded(System.Int32? chid)
		{
			return Query_GetByChid_ExcludeSolvedAndExcluded(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_ExcludeSolvedAndExcluded' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_ExcludeSolvedAndExcluded(int start, int pageLength, System.Int32? chid)
		{
			return Query_GetByChid_ExcludeSolvedAndExcluded(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_ExcludeSolvedAndExcluded' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_ExcludeSolvedAndExcluded(TransactionManager transactionManager, System.Int32? chid)
		{
			return Query_GetByChid_ExcludeSolvedAndExcluded(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_ExcludeSolvedAndExcluded' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_GetByChid_ExcludeSolvedAndExcluded(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbPatient_Get_AeSaeCounts 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_AeSaeCounts' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_AeSaeCounts(System.Int32? patientid)
		{
			return Patient_Get_AeSaeCounts(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_AeSaeCounts' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_AeSaeCounts(int start, int pageLength, System.Int32? patientid)
		{
			return Patient_Get_AeSaeCounts(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Get_AeSaeCounts' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_AeSaeCounts(TransactionManager transactionManager, System.Int32? patientid)
		{
			return Patient_Get_AeSaeCounts(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_AeSaeCounts' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_Get_AeSaeCounts(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbCentre_GetByBADBIRUseridAndTraining 
		
		/// <summary>
		///	This method wrap the 'bbCentre_GetByBADBIRUseridAndTraining' stored procedure. 
		/// </summary>
		/// <param name="badbirUserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_GetByBADBIRUseridAndTraining(System.Int32? badbirUserid, System.Int32? isTraining)
		{
			return Centre_GetByBADBIRUseridAndTraining(null, 0, int.MaxValue , badbirUserid, isTraining);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentre_GetByBADBIRUseridAndTraining' stored procedure. 
		/// </summary>
		/// <param name="badbirUserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_GetByBADBIRUseridAndTraining(int start, int pageLength, System.Int32? badbirUserid, System.Int32? isTraining)
		{
			return Centre_GetByBADBIRUseridAndTraining(null, start, pageLength , badbirUserid, isTraining);
		}
				
		/// <summary>
		///	This method wrap the 'bbCentre_GetByBADBIRUseridAndTraining' stored procedure. 
		/// </summary>
		/// <param name="badbirUserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_GetByBADBIRUseridAndTraining(TransactionManager transactionManager, System.Int32? badbirUserid, System.Int32? isTraining)
		{
			return Centre_GetByBADBIRUseridAndTraining(transactionManager, 0, int.MaxValue , badbirUserid, isTraining);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentre_GetByBADBIRUseridAndTraining' stored procedure. 
		/// </summary>
		/// <param name="badbirUserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTraining"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Centre_GetByBADBIRUseridAndTraining(TransactionManager transactionManager, int start, int pageLength , System.Int32? badbirUserid, System.Int32? isTraining);
		
		#endregion
		
		#region bbQuery_GetByChid_SolvedQueries 
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_SolvedQueries' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_SolvedQueries(System.Int32? chid)
		{
			return Query_GetByChid_SolvedQueries(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_SolvedQueries' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_SolvedQueries(int start, int pageLength, System.Int32? chid)
		{
			return Query_GetByChid_SolvedQueries(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_SolvedQueries' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetByChid_SolvedQueries(TransactionManager transactionManager, System.Int32? chid)
		{
			return Query_GetByChid_SolvedQueries(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetByChid_SolvedQueries' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_GetByChid_SolvedQueries(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbSummaryPage_getPasiData 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getPasiData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getPasiData(System.Int32? patientid)
		{
			return SummaryPage_getPasiData(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getPasiData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getPasiData(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_getPasiData(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getPasiData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getPasiData(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_getPasiData(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getPasiData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_getPasiData(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbExport_Company_Dataset 
		
		/// <summary>
		///	This method wrap the 'bbExport_Company_Dataset' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Export_Company_Dataset()
		{
			 Export_Company_Dataset(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbExport_Company_Dataset' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Export_Company_Dataset(int start, int pageLength)
		{
			 Export_Company_Dataset(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbExport_Company_Dataset' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Export_Company_Dataset(TransactionManager transactionManager)
		{
			 Export_Company_Dataset(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbExport_Company_Dataset' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void Export_Company_Dataset(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbDelegationLog_isUserPIAtCentre 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_isUserPIAtCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_isUserPIAtCentre(System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_isUserPIAtCentre(null, 0, int.MaxValue , badbirUserId, centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_isUserPIAtCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_isUserPIAtCentre(int start, int pageLength, System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_isUserPIAtCentre(null, start, pageLength , badbirUserId, centreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLog_isUserPIAtCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_isUserPIAtCentre(TransactionManager transactionManager, System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_isUserPIAtCentre(transactionManager, 0, int.MaxValue , badbirUserId, centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_isUserPIAtCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLog_isUserPIAtCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? badbirUserId, System.Int32? centreId);
		
		#endregion
		
		#region bbDruglkp_GetDrugsAtEventStart 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_GetDrugsAtEventStart' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="eventStart"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_GetDrugsAtEventStart(System.Int32? fupid, System.DateTime? eventStart)
		{
			return Druglkp_GetDrugsAtEventStart(null, 0, int.MaxValue , fupid, eventStart);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_GetDrugsAtEventStart' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="eventStart"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_GetDrugsAtEventStart(int start, int pageLength, System.Int32? fupid, System.DateTime? eventStart)
		{
			return Druglkp_GetDrugsAtEventStart(null, start, pageLength , fupid, eventStart);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_GetDrugsAtEventStart' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="eventStart"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_GetDrugsAtEventStart(TransactionManager transactionManager, System.Int32? fupid, System.DateTime? eventStart)
		{
			return Druglkp_GetDrugsAtEventStart(transactionManager, 0, int.MaxValue , fupid, eventStart);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_GetDrugsAtEventStart' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="eventStart"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_GetDrugsAtEventStart(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupid, System.DateTime? eventStart);
		
		#endregion
		
		#region bbAutobotFUPQueryCheck 
		
		/// <summary>
		///	This method wrap the 'bbAutobotFUPQueryCheck' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AutobotFUPQueryCheck(System.Int32? fupid)
		{
			return AutobotFUPQueryCheck(null, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAutobotFUPQueryCheck' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AutobotFUPQueryCheck(int start, int pageLength, System.Int32? fupid)
		{
			return AutobotFUPQueryCheck(null, start, pageLength , fupid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAutobotFUPQueryCheck' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AutobotFUPQueryCheck(TransactionManager transactionManager, System.Int32? fupid)
		{
			return AutobotFUPQueryCheck(transactionManager, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAutobotFUPQueryCheck' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AutobotFUPQueryCheck(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupid);
		
		#endregion
		
		#region bbPatientDrug_AdminUpdateConMedDetails 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminUpdateConMedDetails' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateuserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateusername"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminUpdateConMedDetails(System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Int32? newdrugid, System.Int32? newstartday, System.Int32? newstartmonth, System.Int32? newstartyear, System.Int32? updateuserid, System.String updateusername)
		{
			 PatientDrug_AdminUpdateConMedDetails(null, 0, int.MaxValue , chid, drugid, startday, startmonth, startyear, newdrugid, newstartday, newstartmonth, newstartyear, updateuserid, updateusername);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminUpdateConMedDetails' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateuserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateusername"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminUpdateConMedDetails(int start, int pageLength, System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Int32? newdrugid, System.Int32? newstartday, System.Int32? newstartmonth, System.Int32? newstartyear, System.Int32? updateuserid, System.String updateusername)
		{
			 PatientDrug_AdminUpdateConMedDetails(null, start, pageLength , chid, drugid, startday, startmonth, startyear, newdrugid, newstartday, newstartmonth, newstartyear, updateuserid, updateusername);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminUpdateConMedDetails' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateuserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateusername"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminUpdateConMedDetails(TransactionManager transactionManager, System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Int32? newdrugid, System.Int32? newstartday, System.Int32? newstartmonth, System.Int32? newstartyear, System.Int32? updateuserid, System.String updateusername)
		{
			 PatientDrug_AdminUpdateConMedDetails(transactionManager, 0, int.MaxValue , chid, drugid, startday, startmonth, startyear, newdrugid, newstartday, newstartmonth, newstartyear, updateuserid, updateusername);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminUpdateConMedDetails' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="newstartyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateuserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="updateusername"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void PatientDrug_AdminUpdateConMedDetails(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Int32? newdrugid, System.Int32? newstartday, System.Int32? newstartmonth, System.Int32? newstartyear, System.Int32? updateuserid, System.String updateusername);
		
		#endregion
		
		#region bbCloseEditWindowByFupid 
		
		/// <summary>
		///	This method wrap the 'bbCloseEditWindowByFupid' stored procedure. 
		/// </summary>
		/// <param name="thisFupid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CloseEditWindowByFupid(System.Int32? thisFupid)
		{
			return CloseEditWindowByFupid(null, 0, int.MaxValue , thisFupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbCloseEditWindowByFupid' stored procedure. 
		/// </summary>
		/// <param name="thisFupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CloseEditWindowByFupid(int start, int pageLength, System.Int32? thisFupid)
		{
			return CloseEditWindowByFupid(null, start, pageLength , thisFupid);
		}
				
		/// <summary>
		///	This method wrap the 'bbCloseEditWindowByFupid' stored procedure. 
		/// </summary>
		/// <param name="thisFupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CloseEditWindowByFupid(TransactionManager transactionManager, System.Int32? thisFupid)
		{
			return CloseEditWindowByFupid(transactionManager, 0, int.MaxValue , thisFupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbCloseEditWindowByFupid' stored procedure. 
		/// </summary>
		/// <param name="thisFupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet CloseEditWindowByFupid(TransactionManager transactionManager, int start, int pageLength , System.Int32? thisFupid);
		
		#endregion
		
		#region bbAEEsi_NotFilledLkp 
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledLkp' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledLkp(System.Int32? patientid)
		{
			return AEEsi_NotFilledLkp(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledLkp' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledLkp(int start, int pageLength, System.Int32? patientid)
		{
			return AEEsi_NotFilledLkp(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledLkp' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledLkp(TransactionManager transactionManager, System.Int32? patientid)
		{
			return AEEsi_NotFilledLkp(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledLkp' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AEEsi_NotFilledLkp(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbQueryForCentre_UnreadQueryCountByCentre 
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_UnreadQueryCountByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTrainer"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_UnreadQueryCountByCentre(System.Int32? centreid, System.Int32? isTrainer)
		{
			return QueryForCentre_UnreadQueryCountByCentre(null, 0, int.MaxValue , centreid, isTrainer);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_UnreadQueryCountByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTrainer"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_UnreadQueryCountByCentre(int start, int pageLength, System.Int32? centreid, System.Int32? isTrainer)
		{
			return QueryForCentre_UnreadQueryCountByCentre(null, start, pageLength , centreid, isTrainer);
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_UnreadQueryCountByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTrainer"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_UnreadQueryCountByCentre(TransactionManager transactionManager, System.Int32? centreid, System.Int32? isTrainer)
		{
			return QueryForCentre_UnreadQueryCountByCentre(transactionManager, 0, int.MaxValue , centreid, isTrainer);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_UnreadQueryCountByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isTrainer"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryForCentre_UnreadQueryCountByCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid, System.Int32? isTrainer);
		
		#endregion
		
		#region bbDelegationLog_getPIDetailsByCentre 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_getPIDetailsByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_getPIDetailsByCentre(System.Int32? centreId)
		{
			return DelegationLog_getPIDetailsByCentre(null, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_getPIDetailsByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_getPIDetailsByCentre(int start, int pageLength, System.Int32? centreId)
		{
			return DelegationLog_getPIDetailsByCentre(null, start, pageLength , centreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLog_getPIDetailsByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_getPIDetailsByCentre(TransactionManager transactionManager, System.Int32? centreId)
		{
			return DelegationLog_getPIDetailsByCentre(transactionManager, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_getPIDetailsByCentre' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLog_getPIDetailsByCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreId);
		
		#endregion
		
		#region bbQueryForCentre_MarkAsAdminRead 
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsAdminRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsAdminRead(System.Int32? qid)
		{
			return QueryForCentre_MarkAsAdminRead(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsAdminRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsAdminRead(int start, int pageLength, System.Int32? qid)
		{
			return QueryForCentre_MarkAsAdminRead(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsAdminRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsAdminRead(TransactionManager transactionManager, System.Int32? qid)
		{
			return QueryForCentre_MarkAsAdminRead(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsAdminRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryForCentre_MarkAsAdminRead(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbAdverseEventReportGeneratorInfo 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventReportGeneratorInfo' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiruserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="companyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="reportid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventReportGeneratorInfo(System.Int32? fupaeid, System.Int32? badbiruserid, System.Int32? companyid, System.Int32? reportid)
		{
			return AdverseEventReportGeneratorInfo(null, 0, int.MaxValue , fupaeid, badbiruserid, companyid, reportid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventReportGeneratorInfo' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiruserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="companyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="reportid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventReportGeneratorInfo(int start, int pageLength, System.Int32? fupaeid, System.Int32? badbiruserid, System.Int32? companyid, System.Int32? reportid)
		{
			return AdverseEventReportGeneratorInfo(null, start, pageLength , fupaeid, badbiruserid, companyid, reportid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventReportGeneratorInfo' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiruserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="companyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="reportid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventReportGeneratorInfo(TransactionManager transactionManager, System.Int32? fupaeid, System.Int32? badbiruserid, System.Int32? companyid, System.Int32? reportid)
		{
			return AdverseEventReportGeneratorInfo(transactionManager, 0, int.MaxValue , fupaeid, badbiruserid, companyid, reportid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventReportGeneratorInfo' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiruserid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="companyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="reportid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventReportGeneratorInfo(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid, System.Int32? badbiruserid, System.Int32? companyid, System.Int32? reportid);
		
		#endregion
		
		#region bbPatientdrug_GetByChid_Biologic 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_Biologic' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_Biologic(System.Int32? chid)
		{
			return Patientdrug_GetByChid_Biologic(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_Biologic' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_Biologic(int start, int pageLength, System.Int32? chid)
		{
			return Patientdrug_GetByChid_Biologic(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_Biologic' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_Biologic(TransactionManager transactionManager, System.Int32? chid)
		{
			return Patientdrug_GetByChid_Biologic(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_Biologic' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patientdrug_GetByChid_Biologic(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbAdverseEventRpt_GetTodaysEncryptionKeyByCompany 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt_GetTodaysEncryptionKeyByCompany' stored procedure. 
		/// </summary>
		/// <param name="companyid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt_GetTodaysEncryptionKeyByCompany(System.Int32? companyid)
		{
			return AdverseEventRpt_GetTodaysEncryptionKeyByCompany(null, 0, int.MaxValue , companyid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt_GetTodaysEncryptionKeyByCompany' stored procedure. 
		/// </summary>
		/// <param name="companyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt_GetTodaysEncryptionKeyByCompany(int start, int pageLength, System.Int32? companyid)
		{
			return AdverseEventRpt_GetTodaysEncryptionKeyByCompany(null, start, pageLength , companyid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt_GetTodaysEncryptionKeyByCompany' stored procedure. 
		/// </summary>
		/// <param name="companyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt_GetTodaysEncryptionKeyByCompany(TransactionManager transactionManager, System.Int32? companyid)
		{
			return AdverseEventRpt_GetTodaysEncryptionKeyByCompany(transactionManager, 0, int.MaxValue , companyid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt_GetTodaysEncryptionKeyByCompany' stored procedure. 
		/// </summary>
		/// <param name="companyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventRpt_GetTodaysEncryptionKeyByCompany(TransactionManager transactionManager, int start, int pageLength , System.Int32? companyid);
		
		#endregion
		
		#region bbAdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra(System.Int32? fupaeid)
		{
			return AdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra(int start, int pageLength, System.Int32? fupaeid)
		{
			return AdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return AdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventMeddra_GetNamesByFupaeid_NotFoundInCurrentMeddra(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbSummaryPage_GetOldSystemicTherapy 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetOldSystemicTherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetOldSystemicTherapy(System.Int32? patientid)
		{
			return SummaryPage_GetOldSystemicTherapy(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetOldSystemicTherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetOldSystemicTherapy(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_GetOldSystemicTherapy(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetOldSystemicTherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetOldSystemicTherapy(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_GetOldSystemicTherapy(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetOldSystemicTherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_GetOldSystemicTherapy(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbSummaryPage_getLatestWeightHeightWaist 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getLatestWeightHeightWaist' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getLatestWeightHeightWaist(System.Int32? patientid)
		{
			return SummaryPage_getLatestWeightHeightWaist(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getLatestWeightHeightWaist' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getLatestWeightHeightWaist(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_getLatestWeightHeightWaist(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getLatestWeightHeightWaist' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getLatestWeightHeightWaist(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_getLatestWeightHeightWaist(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getLatestWeightHeightWaist' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_getLatestWeightHeightWaist(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbSummaryPage_GetOngoingAEs 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetOngoingAEs' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetOngoingAEs(System.Int32? patientid)
		{
			return SummaryPage_GetOngoingAEs(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetOngoingAEs' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetOngoingAEs(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_GetOngoingAEs(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetOngoingAEs' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetOngoingAEs(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_GetOngoingAEs(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetOngoingAEs' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_GetOngoingAEs(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbCommittee_RecruitmentGraphData 
		
		/// <summary>
		///	This method wrap the 'bbCommittee_RecruitmentGraphData' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_RecruitmentGraphData()
		{
			return Committee_RecruitmentGraphData(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCommittee_RecruitmentGraphData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_RecruitmentGraphData(int start, int pageLength)
		{
			return Committee_RecruitmentGraphData(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbCommittee_RecruitmentGraphData' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_RecruitmentGraphData(TransactionManager transactionManager)
		{
			return Committee_RecruitmentGraphData(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCommittee_RecruitmentGraphData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Committee_RecruitmentGraphData(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbSummaryPage_getAllDlqiValues 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getAllDlqiValues' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getAllDlqiValues(System.Int32? patientid)
		{
			return SummaryPage_getAllDlqiValues(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getAllDlqiValues' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getAllDlqiValues(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_getAllDlqiValues(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getAllDlqiValues' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_getAllDlqiValues(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_getAllDlqiValues(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_getAllDlqiValues' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_getAllDlqiValues(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbUserPreRegistration_GetByBADBIRUserID 
		
		/// <summary>
		///	This method wrap the 'bbUserPreRegistration_GetByBADBIRUserID' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UserPreRegistration_GetByBADBIRUserID(System.Int32? badbirUserId)
		{
			return UserPreRegistration_GetByBADBIRUserID(null, 0, int.MaxValue , badbirUserId);
		}
		
		/// <summary>
		///	This method wrap the 'bbUserPreRegistration_GetByBADBIRUserID' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UserPreRegistration_GetByBADBIRUserID(int start, int pageLength, System.Int32? badbirUserId)
		{
			return UserPreRegistration_GetByBADBIRUserID(null, start, pageLength , badbirUserId);
		}
				
		/// <summary>
		///	This method wrap the 'bbUserPreRegistration_GetByBADBIRUserID' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UserPreRegistration_GetByBADBIRUserID(TransactionManager transactionManager, System.Int32? badbirUserId)
		{
			return UserPreRegistration_GetByBADBIRUserID(transactionManager, 0, int.MaxValue , badbirUserId);
		}
		
		/// <summary>
		///	This method wrap the 'bbUserPreRegistration_GetByBADBIRUserID' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet UserPreRegistration_GetByBADBIRUserID(TransactionManager transactionManager, int start, int pageLength , System.Int32? badbirUserId);
		
		#endregion
		
		#region bbPatientCohortSwitch_PostSwitchActions 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_PostSwitchActions' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_PostSwitchActions(System.Int32? patientid)
		{
			return PatientCohortSwitch_PostSwitchActions(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_PostSwitchActions' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_PostSwitchActions(int start, int pageLength, System.Int32? patientid)
		{
			return PatientCohortSwitch_PostSwitchActions(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_PostSwitchActions' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_PostSwitchActions(TransactionManager transactionManager, System.Int32? patientid)
		{
			return PatientCohortSwitch_PostSwitchActions(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_PostSwitchActions' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortSwitch_PostSwitchActions(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbAdverseEventFup_GetFiledAEsByPatientId 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetFiledAEsByPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetFiledAEsByPatientId(System.Int32? patientId)
		{
			return AdverseEventFup_GetFiledAEsByPatientId(null, 0, int.MaxValue , patientId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetFiledAEsByPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetFiledAEsByPatientId(int start, int pageLength, System.Int32? patientId)
		{
			return AdverseEventFup_GetFiledAEsByPatientId(null, start, pageLength , patientId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetFiledAEsByPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetFiledAEsByPatientId(TransactionManager transactionManager, System.Int32? patientId)
		{
			return AdverseEventFup_GetFiledAEsByPatientId(transactionManager, 0, int.MaxValue , patientId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetFiledAEsByPatientId' stored procedure. 
		/// </summary>
		/// <param name="patientId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_GetFiledAEsByPatientId(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientId);
		
		#endregion
		
		#region bbDelegationLogLatestRows_getByCentreID 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRows_getByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRows_getByCentreID(System.Int32? centreId)
		{
			return DelegationLogLatestRows_getByCentreID(null, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRows_getByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRows_getByCentreID(int start, int pageLength, System.Int32? centreId)
		{
			return DelegationLogLatestRows_getByCentreID(null, start, pageLength , centreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRows_getByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLogLatestRows_getByCentreID(TransactionManager transactionManager, System.Int32? centreId)
		{
			return DelegationLogLatestRows_getByCentreID(transactionManager, 0, int.MaxValue , centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLogLatestRows_getByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLogLatestRows_getByCentreID(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreId);
		
		#endregion
		
		#region bbQuery_UnreadQueryCountByCentre_AdverseEvents 
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_AdverseEvents' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_AdverseEvents(System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_AdverseEvents(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_AdverseEvents' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_AdverseEvents(int start, int pageLength, System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_AdverseEvents(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_AdverseEvents' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_AdverseEvents(TransactionManager transactionManager, System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_AdverseEvents(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_AdverseEvents' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_UnreadQueryCountByCentre_AdverseEvents(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbQuery_MarkAsAdminUnread 
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsAdminUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsAdminUnread(System.Int32? qid)
		{
			return Query_MarkAsAdminUnread(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsAdminUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsAdminUnread(int start, int pageLength, System.Int32? qid)
		{
			return Query_MarkAsAdminUnread(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsAdminUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsAdminUnread(TransactionManager transactionManager, System.Int32? qid)
		{
			return Query_MarkAsAdminUnread(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsAdminUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_MarkAsAdminUnread(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbAdverseEventRpt24FirstRpt 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24FirstRpt' stored procedure. 
		/// </summary>
		/// <param name="companyId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt24FirstRpt(System.Int32? companyId, System.Int32? drugId)
		{
			return AdverseEventRpt24FirstRpt(null, 0, int.MaxValue , companyId, drugId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24FirstRpt' stored procedure. 
		/// </summary>
		/// <param name="companyId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt24FirstRpt(int start, int pageLength, System.Int32? companyId, System.Int32? drugId)
		{
			return AdverseEventRpt24FirstRpt(null, start, pageLength , companyId, drugId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24FirstRpt' stored procedure. 
		/// </summary>
		/// <param name="companyId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRpt24FirstRpt(TransactionManager transactionManager, System.Int32? companyId, System.Int32? drugId)
		{
			return AdverseEventRpt24FirstRpt(transactionManager, 0, int.MaxValue , companyId, drugId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24FirstRpt' stored procedure. 
		/// </summary>
		/// <param name="companyId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventRpt24FirstRpt(TransactionManager transactionManager, int start, int pageLength , System.Int32? companyId, System.Int32? drugId);
		
		#endregion
		
		#region bbDruglkp_Get_List_SM 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM()
		{
			return Druglkp_Get_List_SM(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM(int start, int pageLength)
		{
			return Druglkp_Get_List_SM(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM(TransactionManager transactionManager)
		{
			return Druglkp_Get_List_SM(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_SM(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbPatientdrug_GetByFupId_Biologic 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Biologic' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Biologic(System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Biologic(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Biologic' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Biologic(int start, int pageLength, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Biologic(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Biologic' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Biologic(TransactionManager transactionManager, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Biologic(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Biologic' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public abstract TList<BbPatientdrug> Patientdrug_GetByFupId_Biologic(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbPatientDrug_AdminGetConMedSummaryForEdit 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminGetConMedSummaryForEdit' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_AdminGetConMedSummaryForEdit(System.Int32? patientid)
		{
			return PatientDrug_AdminGetConMedSummaryForEdit(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminGetConMedSummaryForEdit' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_AdminGetConMedSummaryForEdit(int start, int pageLength, System.Int32? patientid)
		{
			return PatientDrug_AdminGetConMedSummaryForEdit(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminGetConMedSummaryForEdit' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientDrug_AdminGetConMedSummaryForEdit(TransactionManager transactionManager, System.Int32? patientid)
		{
			return PatientDrug_AdminGetConMedSummaryForEdit(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminGetConMedSummaryForEdit' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientDrug_AdminGetConMedSummaryForEdit(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbAdverseEventRpt24_ImportReportsFromOldSystem___DELETE 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_ImportReportsFromOldSystem___DELETE' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdverseEventRpt24_ImportReportsFromOldSystem___DELETE()
		{
			 AdverseEventRpt24_ImportReportsFromOldSystem___DELETE(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_ImportReportsFromOldSystem___DELETE' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdverseEventRpt24_ImportReportsFromOldSystem___DELETE(int start, int pageLength)
		{
			 AdverseEventRpt24_ImportReportsFromOldSystem___DELETE(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_ImportReportsFromOldSystem___DELETE' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdverseEventRpt24_ImportReportsFromOldSystem___DELETE(TransactionManager transactionManager)
		{
			 AdverseEventRpt24_ImportReportsFromOldSystem___DELETE(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRpt24_ImportReportsFromOldSystem___DELETE' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void AdverseEventRpt24_ImportReportsFromOldSystem___DELETE(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbDruglkp_Get_List_SM_For_patdrugid_Baseline 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM_For_patdrugid_Baseline(System.Int32? patdrugid, System.Int32? isAdmin, System.Int32? fupid)
		{
			return Druglkp_Get_List_SM_For_patdrugid_Baseline(null, 0, int.MaxValue , patdrugid, isAdmin, fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM_For_patdrugid_Baseline(int start, int pageLength, System.Int32? patdrugid, System.Int32? isAdmin, System.Int32? fupid)
		{
			return Druglkp_Get_List_SM_For_patdrugid_Baseline(null, start, pageLength , patdrugid, isAdmin, fupid);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_SM_For_patdrugid_Baseline(TransactionManager transactionManager, System.Int32? patdrugid, System.Int32? isAdmin, System.Int32? fupid)
		{
			return Druglkp_Get_List_SM_For_patdrugid_Baseline(transactionManager, 0, int.MaxValue , patdrugid, isAdmin, fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_SM_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_SM_For_patdrugid_Baseline(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid, System.Int32? isAdmin, System.Int32? fupid);
		
		#endregion
		
		#region bbDruglkp_Get_List_Biologic_For_patdrugid_Fup 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic_For_patdrugid_Fup' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic_For_patdrugid_Fup(System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Biologic_For_patdrugid_Fup(null, 0, int.MaxValue , patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic_For_patdrugid_Fup' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic_For_patdrugid_Fup(int start, int pageLength, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Biologic_For_patdrugid_Fup(null, start, pageLength , patdrugid, isAdmin);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic_For_patdrugid_Fup' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic_For_patdrugid_Fup(TransactionManager transactionManager, System.Int32? patdrugid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Biologic_For_patdrugid_Fup(transactionManager, 0, int.MaxValue , patdrugid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic_For_patdrugid_Fup' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Biologic_For_patdrugid_Fup(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid, System.Int32? isAdmin);
		
		#endregion
		
		#region bbDruglkp_Get_List_Conventional_For_patdrugid_FUP 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid_FUP' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid_FUP(System.Int32? patdrugid, System.Int32? fupid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid_FUP(null, 0, int.MaxValue , patdrugid, fupid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid_FUP' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid_FUP(int start, int pageLength, System.Int32? patdrugid, System.Int32? fupid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid_FUP(null, start, pageLength , patdrugid, fupid, isAdmin);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid_FUP' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid_FUP(TransactionManager transactionManager, System.Int32? patdrugid, System.Int32? fupid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid_FUP(transactionManager, 0, int.MaxValue , patdrugid, fupid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid_FUP' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Conventional_For_patdrugid_FUP(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid, System.Int32? fupid, System.Int32? isAdmin);
		
		#endregion
		
		#region bbPatientdrug_GetByChid_Conventional 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_Conventional' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_Conventional(System.Int32? chid)
		{
			return Patientdrug_GetByChid_Conventional(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_Conventional' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_Conventional(int start, int pageLength, System.Int32? chid)
		{
			return Patientdrug_GetByChid_Conventional(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_Conventional' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_Conventional(TransactionManager transactionManager, System.Int32? chid)
		{
			return Patientdrug_GetByChid_Conventional(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_Conventional' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patientdrug_GetByChid_Conventional(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbCompanyAudit_Insert 
		
		/// <summary>
		///	This method wrap the 'bbCompanyAudit_Insert' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.Guid?</c> instance.</param>
		/// <param name="username"> A <c>System.String</c> instance.</param>
		/// <param name="ipaddress"> A <c>System.String</c> instance.</param>
		/// <param name="date"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="pageDetail"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void CompanyAudit_Insert(System.Guid? userid, System.String username, System.String ipaddress, System.DateTime? date, System.String pageDetail)
		{
			 CompanyAudit_Insert(null, 0, int.MaxValue , userid, username, ipaddress, date, pageDetail);
		}
		
		/// <summary>
		///	This method wrap the 'bbCompanyAudit_Insert' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.Guid?</c> instance.</param>
		/// <param name="username"> A <c>System.String</c> instance.</param>
		/// <param name="ipaddress"> A <c>System.String</c> instance.</param>
		/// <param name="date"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="pageDetail"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void CompanyAudit_Insert(int start, int pageLength, System.Guid? userid, System.String username, System.String ipaddress, System.DateTime? date, System.String pageDetail)
		{
			 CompanyAudit_Insert(null, start, pageLength , userid, username, ipaddress, date, pageDetail);
		}
				
		/// <summary>
		///	This method wrap the 'bbCompanyAudit_Insert' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.Guid?</c> instance.</param>
		/// <param name="username"> A <c>System.String</c> instance.</param>
		/// <param name="ipaddress"> A <c>System.String</c> instance.</param>
		/// <param name="date"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="pageDetail"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void CompanyAudit_Insert(TransactionManager transactionManager, System.Guid? userid, System.String username, System.String ipaddress, System.DateTime? date, System.String pageDetail)
		{
			 CompanyAudit_Insert(transactionManager, 0, int.MaxValue , userid, username, ipaddress, date, pageDetail);
		}
		
		/// <summary>
		///	This method wrap the 'bbCompanyAudit_Insert' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.Guid?</c> instance.</param>
		/// <param name="username"> A <c>System.String</c> instance.</param>
		/// <param name="ipaddress"> A <c>System.String</c> instance.</param>
		/// <param name="date"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="pageDetail"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void CompanyAudit_Insert(TransactionManager transactionManager, int start, int pageLength , System.Guid? userid, System.String username, System.String ipaddress, System.DateTime? date, System.String pageDetail);
		
		#endregion
		
		#region bbPatientCohortTracking_getFupNosByChid 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_getFupNosByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_getFupNosByChid(System.Int32? chid)
		{
			return PatientCohortTracking_getFupNosByChid(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_getFupNosByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_getFupNosByChid(int start, int pageLength, System.Int32? chid)
		{
			return PatientCohortTracking_getFupNosByChid(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_getFupNosByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_getFupNosByChid(TransactionManager transactionManager, System.Int32? chid)
		{
			return PatientCohortTracking_getFupNosByChid(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_getFupNosByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortTracking_getFupNosByChid(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbSummaryPage_GetPastAEs 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetPastAEs' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetPastAEs(System.Int32? patientid)
		{
			return SummaryPage_GetPastAEs(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetPastAEs' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetPastAEs(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_GetPastAEs(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetPastAEs' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetPastAEs(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_GetPastAEs(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetPastAEs' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_GetPastAEs(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbPatient_Get_List_byCentre 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_byCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_byCentre(System.Int32? centreid)
		{
			return Patient_Get_List_byCentre(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_byCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_byCentre(int start, int pageLength, System.Int32? centreid)
		{
			return Patient_Get_List_byCentre(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_byCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_byCentre(TransactionManager transactionManager, System.Int32? centreid)
		{
			return Patient_Get_List_byCentre(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_byCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_Get_List_byCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbPatient_Get_BSTOP_PATIENTS_byCentre 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_BSTOP_PATIENTS_byCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_BSTOP_PATIENTS_byCentre(System.Int32? centreid)
		{
			return Patient_Get_BSTOP_PATIENTS_byCentre(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_BSTOP_PATIENTS_byCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_BSTOP_PATIENTS_byCentre(int start, int pageLength, System.Int32? centreid)
		{
			return Patient_Get_BSTOP_PATIENTS_byCentre(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Get_BSTOP_PATIENTS_byCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_BSTOP_PATIENTS_byCentre(TransactionManager transactionManager, System.Int32? centreid)
		{
			return Patient_Get_BSTOP_PATIENTS_byCentre(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_BSTOP_PATIENTS_byCentre' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_Get_BSTOP_PATIENTS_byCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbQueryForCentre_MarkAsClinicianUnread 
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsClinicianUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsClinicianUnread(System.Int32? qid)
		{
			return QueryForCentre_MarkAsClinicianUnread(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsClinicianUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsClinicianUnread(int start, int pageLength, System.Int32? qid)
		{
			return QueryForCentre_MarkAsClinicianUnread(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsClinicianUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryForCentre_MarkAsClinicianUnread(TransactionManager transactionManager, System.Int32? qid)
		{
			return QueryForCentre_MarkAsClinicianUnread(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryForCentre_MarkAsClinicianUnread' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryForCentre_MarkAsClinicianUnread(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbBiologicDrugHistory_GetByFupaeid 
		
		/// <summary>
		///	This method wrap the 'bbBiologicDrugHistory_GetByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet BiologicDrugHistory_GetByFupaeid(System.Int32? fupaeid)
		{
			return BiologicDrugHistory_GetByFupaeid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbBiologicDrugHistory_GetByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet BiologicDrugHistory_GetByFupaeid(int start, int pageLength, System.Int32? fupaeid)
		{
			return BiologicDrugHistory_GetByFupaeid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbBiologicDrugHistory_GetByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet BiologicDrugHistory_GetByFupaeid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return BiologicDrugHistory_GetByFupaeid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbBiologicDrugHistory_GetByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet BiologicDrugHistory_GetByFupaeid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbPatientCohortTracking_FupSwitch 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_FupSwitch' stored procedure. 
		/// </summary>
		/// <param name="srcfupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="targetfupid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_FupSwitch(System.Int32? srcfupid, System.Int32? targetfupid)
		{
			return PatientCohortTracking_FupSwitch(null, 0, int.MaxValue , srcfupid, targetfupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_FupSwitch' stored procedure. 
		/// </summary>
		/// <param name="srcfupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="targetfupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_FupSwitch(int start, int pageLength, System.Int32? srcfupid, System.Int32? targetfupid)
		{
			return PatientCohortTracking_FupSwitch(null, start, pageLength , srcfupid, targetfupid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_FupSwitch' stored procedure. 
		/// </summary>
		/// <param name="srcfupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="targetfupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_FupSwitch(TransactionManager transactionManager, System.Int32? srcfupid, System.Int32? targetfupid)
		{
			return PatientCohortTracking_FupSwitch(transactionManager, 0, int.MaxValue , srcfupid, targetfupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_FupSwitch' stored procedure. 
		/// </summary>
		/// <param name="srcfupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="targetfupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortTracking_FupSwitch(TransactionManager transactionManager, int start, int pageLength , System.Int32? srcfupid, System.Int32? targetfupid);
		
		#endregion
		
		#region bbQuery_UnreadQueryCountByCentre_Followups 
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Followups' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Followups(System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Followups(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Followups' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Followups(int start, int pageLength, System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Followups(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Followups' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Followups(TransactionManager transactionManager, System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Followups(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Followups' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_UnreadQueryCountByCentre_Followups(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbQuery_GetRelatedQueriesByFupAEID 
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetRelatedQueriesByFupAEID' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetRelatedQueriesByFupAEID(System.Int32? fupaeid)
		{
			return Query_GetRelatedQueriesByFupAEID(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetRelatedQueriesByFupAEID' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetRelatedQueriesByFupAEID(int start, int pageLength, System.Int32? fupaeid)
		{
			return Query_GetRelatedQueriesByFupAEID(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_GetRelatedQueriesByFupAEID' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_GetRelatedQueriesByFupAEID(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return Query_GetRelatedQueriesByFupAEID(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_GetRelatedQueriesByFupAEID' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_GetRelatedQueriesByFupAEID(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbAdminTool_SetUserLockout 
		
		/// <summary>
		///	This method wrap the 'bbAdminTool_SetUserLockout' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdminTool_SetUserLockout(System.String userid)
		{
			 AdminTool_SetUserLockout(null, 0, int.MaxValue , userid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdminTool_SetUserLockout' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdminTool_SetUserLockout(int start, int pageLength, System.String userid)
		{
			 AdminTool_SetUserLockout(null, start, pageLength , userid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdminTool_SetUserLockout' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void AdminTool_SetUserLockout(TransactionManager transactionManager, System.String userid)
		{
			 AdminTool_SetUserLockout(transactionManager, 0, int.MaxValue , userid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdminTool_SetUserLockout' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void AdminTool_SetUserLockout(TransactionManager transactionManager, int start, int pageLength , System.String userid);
		
		#endregion
		
		#region bbPharmaLog_GetByChid 
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByChid(System.Int32? chid)
		{
			return PharmaLog_GetByChid(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByChid(int start, int pageLength, System.Int32? chid)
		{
			return PharmaLog_GetByChid(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByChid(TransactionManager transactionManager, System.Int32? chid)
		{
			return PharmaLog_GetByChid(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByChid' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PharmaLog_GetByChid(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbDelegationLog_PIPendingCountForCentre 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_PIPendingCountForCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_PIPendingCountForCentre(System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_PIPendingCountForCentre(null, 0, int.MaxValue , badbirUserId, centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_PIPendingCountForCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_PIPendingCountForCentre(int start, int pageLength, System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_PIPendingCountForCentre(null, start, pageLength , badbirUserId, centreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLog_PIPendingCountForCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_PIPendingCountForCentre(TransactionManager transactionManager, System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_PIPendingCountForCentre(transactionManager, 0, int.MaxValue , badbirUserId, centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_PIPendingCountForCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLog_PIPendingCountForCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? badbirUserId, System.Int32? centreId);
		
		#endregion
		
		#region bbAdditionalUserDetail_getNonAdminUsersByCentreID 
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_getNonAdminUsersByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_getNonAdminUsersByCentreID(System.Int32? centreid)
		{
			return AdditionalUserDetail_getNonAdminUsersByCentreID(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_getNonAdminUsersByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_getNonAdminUsersByCentreID(int start, int pageLength, System.Int32? centreid)
		{
			return AdditionalUserDetail_getNonAdminUsersByCentreID(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_getNonAdminUsersByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_getNonAdminUsersByCentreID(TransactionManager transactionManager, System.Int32? centreid)
		{
			return AdditionalUserDetail_getNonAdminUsersByCentreID(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_getNonAdminUsersByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdditionalUserDetail_getNonAdminUsersByCentreID(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbPharma_SAECountBreakdown 
		
		/// <summary>
		///	This method wrap the 'bbPharma_SAECountBreakdown' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_SAECountBreakdown()
		{
			return Pharma_SAECountBreakdown(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharma_SAECountBreakdown' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_SAECountBreakdown(int start, int pageLength)
		{
			return Pharma_SAECountBreakdown(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbPharma_SAECountBreakdown' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_SAECountBreakdown(TransactionManager transactionManager)
		{
			return Pharma_SAECountBreakdown(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharma_SAECountBreakdown' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Pharma_SAECountBreakdown(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbAdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID(System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID(null, 0, int.MaxValue , aeLinkageRowId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID(int start, int pageLength, System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID(null, start, pageLength , aeLinkageRowId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID(TransactionManager transactionManager, System.Int32? aeLinkageRowId)
		{
			return AdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID(transactionManager, 0, int.MaxValue , aeLinkageRowId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID' stored procedure. 
		/// </summary>
		/// <param name="aeLinkageRowId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventLinkageProcessingDiagnosis_GetAllInstancesByAELinkageRowID(TransactionManager transactionManager, int start, int pageLength , System.Int32? aeLinkageRowId);
		
		#endregion
		
		#region bbPatientCohortTracking_FupDataSummaryForSwitch 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_FupDataSummaryForSwitch' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_FupDataSummaryForSwitch(System.Int32? fupid)
		{
			return PatientCohortTracking_FupDataSummaryForSwitch(null, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_FupDataSummaryForSwitch' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_FupDataSummaryForSwitch(int start, int pageLength, System.Int32? fupid)
		{
			return PatientCohortTracking_FupDataSummaryForSwitch(null, start, pageLength , fupid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_FupDataSummaryForSwitch' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortTracking_FupDataSummaryForSwitch(TransactionManager transactionManager, System.Int32? fupid)
		{
			return PatientCohortTracking_FupDataSummaryForSwitch(transactionManager, 0, int.MaxValue , fupid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortTracking_FupDataSummaryForSwitch' stored procedure. 
		/// </summary>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortTracking_FupDataSummaryForSwitch(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupid);
		
		#endregion
		
		#region bbPharma_DeceasedPatientLists 
		
		/// <summary>
		///	This method wrap the 'bbPharma_DeceasedPatientLists' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_DeceasedPatientLists()
		{
			return Pharma_DeceasedPatientLists(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharma_DeceasedPatientLists' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_DeceasedPatientLists(int start, int pageLength)
		{
			return Pharma_DeceasedPatientLists(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbPharma_DeceasedPatientLists' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_DeceasedPatientLists(TransactionManager transactionManager)
		{
			return Pharma_DeceasedPatientLists(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharma_DeceasedPatientLists' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Pharma_DeceasedPatientLists(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbQuery_MarkAsClinicianRead 
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsClinicianRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsClinicianRead(System.Int32? qid)
		{
			return Query_MarkAsClinicianRead(null, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsClinicianRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsClinicianRead(int start, int pageLength, System.Int32? qid)
		{
			return Query_MarkAsClinicianRead(null, start, pageLength , qid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsClinicianRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_MarkAsClinicianRead(TransactionManager transactionManager, System.Int32? qid)
		{
			return Query_MarkAsClinicianRead(transactionManager, 0, int.MaxValue , qid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_MarkAsClinicianRead' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_MarkAsClinicianRead(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid);
		
		#endregion
		
		#region bbPatientCohortSwitch_getData 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_getData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_getData(System.Int32? patientid)
		{
			return PatientCohortSwitch_getData(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_getData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_getData(int start, int pageLength, System.Int32? patientid)
		{
			return PatientCohortSwitch_getData(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_getData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortSwitch_getData(TransactionManager transactionManager, System.Int32? patientid)
		{
			return PatientCohortSwitch_getData(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortSwitch_getData' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortSwitch_getData(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbPatientDrug_GetByFupaeid_DrugsAtTimeOfEvent 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetByFupaeid_DrugsAtTimeOfEvent' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent(System.Int32? fupaeid)
		{
			return PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetByFupaeid_DrugsAtTimeOfEvent' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent(int start, int pageLength, System.Int32? fupaeid)
		{
			return PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetByFupaeid_DrugsAtTimeOfEvent' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_GetByFupaeid_DrugsAtTimeOfEvent' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public abstract TList<BbPatientdrug> PatientDrug_GetByFupaeid_DrugsAtTimeOfEvent(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbCentre_getAllCentresAndIdsOnly 
		
		/// <summary>
		///	This method wrap the 'bbCentre_getAllCentresAndIdsOnly' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_getAllCentresAndIdsOnly()
		{
			return Centre_getAllCentresAndIdsOnly(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCentre_getAllCentresAndIdsOnly' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_getAllCentresAndIdsOnly(int start, int pageLength)
		{
			return Centre_getAllCentresAndIdsOnly(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbCentre_getAllCentresAndIdsOnly' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Centre_getAllCentresAndIdsOnly(TransactionManager transactionManager)
		{
			return Centre_getAllCentresAndIdsOnly(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCentre_getAllCentresAndIdsOnly' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Centre_getAllCentresAndIdsOnly(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbESIlkp_Get_List_Ordered 
		
		/// <summary>
		///	This method wrap the 'bbESIlkp_Get_List_Ordered' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet ESIlkp_Get_List_Ordered()
		{
			return ESIlkp_Get_List_Ordered(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbESIlkp_Get_List_Ordered' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet ESIlkp_Get_List_Ordered(int start, int pageLength)
		{
			return ESIlkp_Get_List_Ordered(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbESIlkp_Get_List_Ordered' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet ESIlkp_Get_List_Ordered(TransactionManager transactionManager)
		{
			return ESIlkp_Get_List_Ordered(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbESIlkp_Get_List_Ordered' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet ESIlkp_Get_List_Ordered(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbQueryTypelkp_GetAllForClinician 
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForClinician' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForClinician()
		{
			return QueryTypelkp_GetAllForClinician(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForClinician' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForClinician(int start, int pageLength)
		{
			return QueryTypelkp_GetAllForClinician(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForClinician' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet QueryTypelkp_GetAllForClinician(TransactionManager transactionManager)
		{
			return QueryTypelkp_GetAllForClinician(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbQueryTypelkp_GetAllForClinician' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet QueryTypelkp_GetAllForClinician(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbNotification_GetSolved_Custom 
		
		/// <summary>
		///	This method wrap the 'bbNotification_GetSolved_Custom' stored procedure. 
		/// </summary>
		/// <param name="showAdminInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showPvInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showSuperInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="typeList"> A <c>System.String</c> instance.</param>
		/// <param name="studyId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dateFrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateTo"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Notification_GetSolved_Custom(System.Int32? showAdminInbox, System.Int32? showPvInbox, System.Int32? showSuperInbox, System.String typeList, System.Int32? studyId, System.DateTime? dateFrom, System.DateTime? dateTo)
		{
			return Notification_GetSolved_Custom(null, 0, int.MaxValue , showAdminInbox, showPvInbox, showSuperInbox, typeList, studyId, dateFrom, dateTo);
		}
		
		/// <summary>
		///	This method wrap the 'bbNotification_GetSolved_Custom' stored procedure. 
		/// </summary>
		/// <param name="showAdminInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showPvInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showSuperInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="typeList"> A <c>System.String</c> instance.</param>
		/// <param name="studyId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dateFrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateTo"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Notification_GetSolved_Custom(int start, int pageLength, System.Int32? showAdminInbox, System.Int32? showPvInbox, System.Int32? showSuperInbox, System.String typeList, System.Int32? studyId, System.DateTime? dateFrom, System.DateTime? dateTo)
		{
			return Notification_GetSolved_Custom(null, start, pageLength , showAdminInbox, showPvInbox, showSuperInbox, typeList, studyId, dateFrom, dateTo);
		}
				
		/// <summary>
		///	This method wrap the 'bbNotification_GetSolved_Custom' stored procedure. 
		/// </summary>
		/// <param name="showAdminInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showPvInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showSuperInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="typeList"> A <c>System.String</c> instance.</param>
		/// <param name="studyId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dateFrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateTo"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Notification_GetSolved_Custom(TransactionManager transactionManager, System.Int32? showAdminInbox, System.Int32? showPvInbox, System.Int32? showSuperInbox, System.String typeList, System.Int32? studyId, System.DateTime? dateFrom, System.DateTime? dateTo)
		{
			return Notification_GetSolved_Custom(transactionManager, 0, int.MaxValue , showAdminInbox, showPvInbox, showSuperInbox, typeList, studyId, dateFrom, dateTo);
		}
		
		/// <summary>
		///	This method wrap the 'bbNotification_GetSolved_Custom' stored procedure. 
		/// </summary>
		/// <param name="showAdminInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showPvInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="showSuperInbox"> A <c>System.Int32?</c> instance.</param>
		/// <param name="typeList"> A <c>System.String</c> instance.</param>
		/// <param name="studyId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dateFrom"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="dateTo"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Notification_GetSolved_Custom(TransactionManager transactionManager, int start, int pageLength , System.Int32? showAdminInbox, System.Int32? showPvInbox, System.Int32? showSuperInbox, System.String typeList, System.Int32? studyId, System.DateTime? dateFrom, System.DateTime? dateTo);
		
		#endregion
		
		#region bbPatient_Check_Access_forClinician 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Check_Access_forClinician' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiRuserid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="clinicianHasAccessToPatient"> A <c>System.Boolean?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Patient_Check_Access_forClinician(System.Int32? patientid, System.Int32? badbiRuserid, ref System.Boolean? clinicianHasAccessToPatient)
		{
			 Patient_Check_Access_forClinician(null, 0, int.MaxValue , patientid, badbiRuserid, ref clinicianHasAccessToPatient);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Check_Access_forClinician' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiRuserid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="clinicianHasAccessToPatient"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Patient_Check_Access_forClinician(int start, int pageLength, System.Int32? patientid, System.Int32? badbiRuserid, ref System.Boolean? clinicianHasAccessToPatient)
		{
			 Patient_Check_Access_forClinician(null, start, pageLength , patientid, badbiRuserid, ref clinicianHasAccessToPatient);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Check_Access_forClinician' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiRuserid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="clinicianHasAccessToPatient"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void Patient_Check_Access_forClinician(TransactionManager transactionManager, System.Int32? patientid, System.Int32? badbiRuserid, ref System.Boolean? clinicianHasAccessToPatient)
		{
			 Patient_Check_Access_forClinician(transactionManager, 0, int.MaxValue , patientid, badbiRuserid, ref clinicianHasAccessToPatient);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Check_Access_forClinician' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="badbiRuserid"> A <c>System.Int32?</c> instance.</param>
			/// <param name="clinicianHasAccessToPatient"> A <c>System.Boolean?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void Patient_Check_Access_forClinician(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid, System.Int32? badbiRuserid, ref System.Boolean? clinicianHasAccessToPatient);
		
		#endregion
		
		#region bbAdverseEventLinkageProcessing_GetAllInstancesByFupaeid 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessing_GetAllInstancesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessing_GetAllInstancesByFupaeid(System.Int32? fupaeid)
		{
			return AdverseEventLinkageProcessing_GetAllInstancesByFupaeid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessing_GetAllInstancesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessing_GetAllInstancesByFupaeid(int start, int pageLength, System.Int32? fupaeid)
		{
			return AdverseEventLinkageProcessing_GetAllInstancesByFupaeid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessing_GetAllInstancesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventLinkageProcessing_GetAllInstancesByFupaeid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return AdverseEventLinkageProcessing_GetAllInstancesByFupaeid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventLinkageProcessing_GetAllInstancesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventLinkageProcessing_GetAllInstancesByFupaeid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbAEEsi_NotFilledLkpByCentreID 
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledLkpByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledLkpByCentreID(System.Int32? centreid)
		{
			return AEEsi_NotFilledLkpByCentreID(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledLkpByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledLkpByCentreID(int start, int pageLength, System.Int32? centreid)
		{
			return AEEsi_NotFilledLkpByCentreID(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledLkpByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AEEsi_NotFilledLkpByCentreID(TransactionManager transactionManager, System.Int32? centreid)
		{
			return AEEsi_NotFilledLkpByCentreID(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAEEsi_NotFilledLkpByCentreID' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AEEsi_NotFilledLkpByCentreID(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbAdverseEventFup_GetAllInstancesByFupaeid 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetAllInstancesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetAllInstancesByFupaeid(System.Int32? fupaeid)
		{
			return AdverseEventFup_GetAllInstancesByFupaeid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetAllInstancesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetAllInstancesByFupaeid(int start, int pageLength, System.Int32? fupaeid)
		{
			return AdverseEventFup_GetAllInstancesByFupaeid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetAllInstancesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetAllInstancesByFupaeid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return AdverseEventFup_GetAllInstancesByFupaeid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetAllInstancesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_GetAllInstancesByFupaeid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbQuery_UnreadQueryCountByCentre_Feedback 
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Feedback' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Feedback(System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Feedback(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Feedback' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Feedback(int start, int pageLength, System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Feedback(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Feedback' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_UnreadQueryCountByCentre_Feedback(TransactionManager transactionManager, System.Int32? centreid)
		{
			return Query_UnreadQueryCountByCentre_Feedback(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_UnreadQueryCountByCentre_Feedback' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_UnreadQueryCountByCentre_Feedback(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbPatient_Get_List_byCentre_InEditWindow 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_byCentre_InEditWindow' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_byCentre_InEditWindow(System.Int32? centreid)
		{
			return Patient_Get_List_byCentre_InEditWindow(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_byCentre_InEditWindow' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_byCentre_InEditWindow(int start, int pageLength, System.Int32? centreid)
		{
			return Patient_Get_List_byCentre_InEditWindow(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_byCentre_InEditWindow' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_byCentre_InEditWindow(TransactionManager transactionManager, System.Int32? centreid)
		{
			return Patient_Get_List_byCentre_InEditWindow(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_byCentre_InEditWindow' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_Get_List_byCentre_InEditWindow(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbESI_GetListByAEFupid 
		
		/// <summary>
		///	This method wrap the 'bbESI_GetListByAEFupid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void ESI_GetListByAEFupid(System.Int32? fupaeid)
		{
			 ESI_GetListByAEFupid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbESI_GetListByAEFupid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void ESI_GetListByAEFupid(int start, int pageLength, System.Int32? fupaeid)
		{
			 ESI_GetListByAEFupid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbESI_GetListByAEFupid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void ESI_GetListByAEFupid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			 ESI_GetListByAEFupid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbESI_GetListByAEFupid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void ESI_GetListByAEFupid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbSummaryPage_GetComorbidities 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetComorbidities' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetComorbidities(System.Int32? patientid)
		{
			return SummaryPage_GetComorbidities(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetComorbidities' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetComorbidities(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_GetComorbidities(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetComorbidities' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetComorbidities(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_GetComorbidities(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetComorbidities' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_GetComorbidities(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbCommittee_CentreWiseMonthlyRecruitmentBreakup_New 
		
		/// <summary>
		///	This method wrap the 'bbCommittee_CentreWiseMonthlyRecruitmentBreakup_New' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_CentreWiseMonthlyRecruitmentBreakup_New()
		{
			return Committee_CentreWiseMonthlyRecruitmentBreakup_New(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCommittee_CentreWiseMonthlyRecruitmentBreakup_New' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_CentreWiseMonthlyRecruitmentBreakup_New(int start, int pageLength)
		{
			return Committee_CentreWiseMonthlyRecruitmentBreakup_New(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbCommittee_CentreWiseMonthlyRecruitmentBreakup_New' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_CentreWiseMonthlyRecruitmentBreakup_New(TransactionManager transactionManager)
		{
			return Committee_CentreWiseMonthlyRecruitmentBreakup_New(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCommittee_CentreWiseMonthlyRecruitmentBreakup_New' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Committee_CentreWiseMonthlyRecruitmentBreakup_New(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbAdverseEventRelatedToDruglkp_GetListByFupaeid 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRelatedToDruglkp_GetListByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRelatedToDruglkp_GetListByFupaeid(System.Int32? fupaeid)
		{
			return AdverseEventRelatedToDruglkp_GetListByFupaeid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRelatedToDruglkp_GetListByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRelatedToDruglkp_GetListByFupaeid(int start, int pageLength, System.Int32? fupaeid)
		{
			return AdverseEventRelatedToDruglkp_GetListByFupaeid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventRelatedToDruglkp_GetListByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventRelatedToDruglkp_GetListByFupaeid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return AdverseEventRelatedToDruglkp_GetListByFupaeid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventRelatedToDruglkp_GetListByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventRelatedToDruglkp_GetListByFupaeid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbQuery_RecalculateMessageCount 
		
		/// <summary>
		///	This method wrap the 'bbQuery_RecalculateMessageCount' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_RecalculateMessageCount(System.Int32? qid, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			return Query_RecalculateMessageCount(null, 0, int.MaxValue , qid, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_RecalculateMessageCount' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_RecalculateMessageCount(int start, int pageLength, System.Int32? qid, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			return Query_RecalculateMessageCount(null, start, pageLength , qid, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
				
		/// <summary>
		///	This method wrap the 'bbQuery_RecalculateMessageCount' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Query_RecalculateMessageCount(TransactionManager transactionManager, System.Int32? qid, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate)
		{
			return Query_RecalculateMessageCount(transactionManager, 0, int.MaxValue , qid, lastupdatedbyid, lastupdatedbyname, lastupdateddate);
		}
		
		/// <summary>
		///	This method wrap the 'bbQuery_RecalculateMessageCount' stored procedure. 
		/// </summary>
		/// <param name="qid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="lastupdatedbyname"> A <c>System.String</c> instance.</param>
		/// <param name="lastupdateddate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Query_RecalculateMessageCount(TransactionManager transactionManager, int start, int pageLength , System.Int32? qid, System.Int32? lastupdatedbyid, System.String lastupdatedbyname, System.DateTime? lastupdateddate);
		
		#endregion
		
		#region bbCentreAuditSummaryOriginal_GetByCentreid 
		
		/// <summary>
		///	This method wrap the 'bbCentreAuditSummaryOriginal_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreAuditSummaryOriginal_GetByCentreid(System.Int32? centreid)
		{
			return CentreAuditSummaryOriginal_GetByCentreid(null, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentreAuditSummaryOriginal_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreAuditSummaryOriginal_GetByCentreid(int start, int pageLength, System.Int32? centreid)
		{
			return CentreAuditSummaryOriginal_GetByCentreid(null, start, pageLength , centreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbCentreAuditSummaryOriginal_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CentreAuditSummaryOriginal_GetByCentreid(TransactionManager transactionManager, System.Int32? centreid)
		{
			return CentreAuditSummaryOriginal_GetByCentreid(transactionManager, 0, int.MaxValue , centreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbCentreAuditSummaryOriginal_GetByCentreid' stored procedure. 
		/// </summary>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet CentreAuditSummaryOriginal_GetByCentreid(TransactionManager transactionManager, int start, int pageLength , System.Int32? centreid);
		
		#endregion
		
		#region bbPatientdrug_GetByChid_SM 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_SM' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_SM(System.Int32? chid)
		{
			return Patientdrug_GetByChid_SM(null, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_SM' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_SM(int start, int pageLength, System.Int32? chid)
		{
			return Patientdrug_GetByChid_SM(null, start, pageLength , chid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_SM' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patientdrug_GetByChid_SM(TransactionManager transactionManager, System.Int32? chid)
		{
			return Patientdrug_GetByChid_SM(transactionManager, 0, int.MaxValue , chid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByChid_SM' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patientdrug_GetByChid_SM(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid);
		
		#endregion
		
		#region bbPatient_Get_NHSICNotSent 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_NHSICNotSent' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_NHSICNotSent()
		{
			return Patient_Get_NHSICNotSent(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_NHSICNotSent' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_NHSICNotSent(int start, int pageLength)
		{
			return Patient_Get_NHSICNotSent(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Get_NHSICNotSent' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_NHSICNotSent(TransactionManager transactionManager)
		{
			return Patient_Get_NHSICNotSent(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_NHSICNotSent' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_Get_NHSICNotSent(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbUKCRNDataExtract_Yearly 
		
		/// <summary>
		///	This method wrap the 'bbUKCRNDataExtract_Yearly' stored procedure. 
		/// </summary>
		/// <param name="entyear"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UKCRNDataExtract_Yearly(System.Int32? entyear)
		{
			return UKCRNDataExtract_Yearly(null, 0, int.MaxValue , entyear);
		}
		
		/// <summary>
		///	This method wrap the 'bbUKCRNDataExtract_Yearly' stored procedure. 
		/// </summary>
		/// <param name="entyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UKCRNDataExtract_Yearly(int start, int pageLength, System.Int32? entyear)
		{
			return UKCRNDataExtract_Yearly(null, start, pageLength , entyear);
		}
				
		/// <summary>
		///	This method wrap the 'bbUKCRNDataExtract_Yearly' stored procedure. 
		/// </summary>
		/// <param name="entyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet UKCRNDataExtract_Yearly(TransactionManager transactionManager, System.Int32? entyear)
		{
			return UKCRNDataExtract_Yearly(transactionManager, 0, int.MaxValue , entyear);
		}
		
		/// <summary>
		///	This method wrap the 'bbUKCRNDataExtract_Yearly' stored procedure. 
		/// </summary>
		/// <param name="entyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet UKCRNDataExtract_Yearly(TransactionManager transactionManager, int start, int pageLength , System.Int32? entyear);
		
		#endregion
		
		#region bbPatient_Get_List_With_Followups_byCentre 
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_With_Followups_byCentre' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="flagDueInNextNdays"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_With_Followups_byCentre(System.Int32? patientid, System.Int32? centreid, System.Int32? flagDueInNextNdays)
		{
			return Patient_Get_List_With_Followups_byCentre(null, 0, int.MaxValue , patientid, centreid, flagDueInNextNdays);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_With_Followups_byCentre' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="flagDueInNextNdays"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_With_Followups_byCentre(int start, int pageLength, System.Int32? patientid, System.Int32? centreid, System.Int32? flagDueInNextNdays)
		{
			return Patient_Get_List_With_Followups_byCentre(null, start, pageLength , patientid, centreid, flagDueInNextNdays);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_With_Followups_byCentre' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="flagDueInNextNdays"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Patient_Get_List_With_Followups_byCentre(TransactionManager transactionManager, System.Int32? patientid, System.Int32? centreid, System.Int32? flagDueInNextNdays)
		{
			return Patient_Get_List_With_Followups_byCentre(transactionManager, 0, int.MaxValue , patientid, centreid, flagDueInNextNdays);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatient_Get_List_With_Followups_byCentre' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="flagDueInNextNdays"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Patient_Get_List_With_Followups_byCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid, System.Int32? centreid, System.Int32? flagDueInNextNdays);
		
		#endregion
		
		#region bbGetLeaveManagerAuthID 
		
		/// <summary>
		///	This method wrap the 'bbGetLeaveManagerAuthID' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetLeaveManagerAuthID(System.Int32? userid)
		{
			return GetLeaveManagerAuthID(null, 0, int.MaxValue , userid);
		}
		
		/// <summary>
		///	This method wrap the 'bbGetLeaveManagerAuthID' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetLeaveManagerAuthID(int start, int pageLength, System.Int32? userid)
		{
			return GetLeaveManagerAuthID(null, start, pageLength , userid);
		}
				
		/// <summary>
		///	This method wrap the 'bbGetLeaveManagerAuthID' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet GetLeaveManagerAuthID(TransactionManager transactionManager, System.Int32? userid)
		{
			return GetLeaveManagerAuthID(transactionManager, 0, int.MaxValue , userid);
		}
		
		/// <summary>
		///	This method wrap the 'bbGetLeaveManagerAuthID' stored procedure. 
		/// </summary>
		/// <param name="userid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet GetLeaveManagerAuthID(TransactionManager transactionManager, int start, int pageLength , System.Int32? userid);
		
		#endregion
		
		#region bbPatientCohortHistory_getStudyNoAndChidByRegCentreId 
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortHistory_getStudyNoAndChidByRegCentreId' stored procedure. 
		/// </summary>
		/// <param name="regcentreid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortHistory_getStudyNoAndChidByRegCentreId(System.Int32? regcentreid)
		{
			return PatientCohortHistory_getStudyNoAndChidByRegCentreId(null, 0, int.MaxValue , regcentreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortHistory_getStudyNoAndChidByRegCentreId' stored procedure. 
		/// </summary>
		/// <param name="regcentreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortHistory_getStudyNoAndChidByRegCentreId(int start, int pageLength, System.Int32? regcentreid)
		{
			return PatientCohortHistory_getStudyNoAndChidByRegCentreId(null, start, pageLength , regcentreid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientCohortHistory_getStudyNoAndChidByRegCentreId' stored procedure. 
		/// </summary>
		/// <param name="regcentreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientCohortHistory_getStudyNoAndChidByRegCentreId(TransactionManager transactionManager, System.Int32? regcentreid)
		{
			return PatientCohortHistory_getStudyNoAndChidByRegCentreId(transactionManager, 0, int.MaxValue , regcentreid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientCohortHistory_getStudyNoAndChidByRegCentreId' stored procedure. 
		/// </summary>
		/// <param name="regcentreid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientCohortHistory_getStudyNoAndChidByRegCentreId(TransactionManager transactionManager, int start, int pageLength , System.Int32? regcentreid);
		
		#endregion
		
		#region bbPatientDrug_AdminDeleteDrugs 
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminDeleteDrugs' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dose"> A <c>System.Double?</c> instance.</param>
		/// <param name="doseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="commonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminDeleteDrugs(System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Double? dose, System.Int32? doseunitid, System.Int32? commonfreqid)
		{
			 PatientDrug_AdminDeleteDrugs(null, 0, int.MaxValue , chid, drugid, startday, startmonth, startyear, dose, doseunitid, commonfreqid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminDeleteDrugs' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dose"> A <c>System.Double?</c> instance.</param>
		/// <param name="doseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="commonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminDeleteDrugs(int start, int pageLength, System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Double? dose, System.Int32? doseunitid, System.Int32? commonfreqid)
		{
			 PatientDrug_AdminDeleteDrugs(null, start, pageLength , chid, drugid, startday, startmonth, startyear, dose, doseunitid, commonfreqid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminDeleteDrugs' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dose"> A <c>System.Double?</c> instance.</param>
		/// <param name="doseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="commonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public void PatientDrug_AdminDeleteDrugs(TransactionManager transactionManager, System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Double? dose, System.Int32? doseunitid, System.Int32? commonfreqid)
		{
			 PatientDrug_AdminDeleteDrugs(transactionManager, 0, int.MaxValue , chid, drugid, startday, startmonth, startyear, dose, doseunitid, commonfreqid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientDrug_AdminDeleteDrugs' stored procedure. 
		/// </summary>
		/// <param name="chid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startday"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startmonth"> A <c>System.Int32?</c> instance.</param>
		/// <param name="startyear"> A <c>System.Int32?</c> instance.</param>
		/// <param name="dose"> A <c>System.Double?</c> instance.</param>
		/// <param name="doseunitid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="commonfreqid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		public abstract void PatientDrug_AdminDeleteDrugs(TransactionManager transactionManager, int start, int pageLength , System.Int32? chid, System.Int32? drugid, System.Int32? startday, System.Int32? startmonth, System.Int32? startyear, System.Double? dose, System.Int32? doseunitid, System.Int32? commonfreqid);
		
		#endregion
		
		#region bbAdverseEventMeddra_SearchTermsByKeyword 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_SearchTermsByKeyword' stored procedure. 
		/// </summary>
		/// <param name="searchTerm"> A <c>System.String</c> instance.</param>
		/// <param name="useSoc"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useHlgt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useHlt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="usePt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useLlt"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_SearchTermsByKeyword(System.String searchTerm, System.Int32? useSoc, System.Int32? useHlgt, System.Int32? useHlt, System.Int32? usePt, System.Int32? useLlt)
		{
			return AdverseEventMeddra_SearchTermsByKeyword(null, 0, int.MaxValue , searchTerm, useSoc, useHlgt, useHlt, usePt, useLlt);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_SearchTermsByKeyword' stored procedure. 
		/// </summary>
		/// <param name="searchTerm"> A <c>System.String</c> instance.</param>
		/// <param name="useSoc"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useHlgt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useHlt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="usePt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useLlt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_SearchTermsByKeyword(int start, int pageLength, System.String searchTerm, System.Int32? useSoc, System.Int32? useHlgt, System.Int32? useHlt, System.Int32? usePt, System.Int32? useLlt)
		{
			return AdverseEventMeddra_SearchTermsByKeyword(null, start, pageLength , searchTerm, useSoc, useHlgt, useHlt, usePt, useLlt);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_SearchTermsByKeyword' stored procedure. 
		/// </summary>
		/// <param name="searchTerm"> A <c>System.String</c> instance.</param>
		/// <param name="useSoc"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useHlgt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useHlt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="usePt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useLlt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_SearchTermsByKeyword(TransactionManager transactionManager, System.String searchTerm, System.Int32? useSoc, System.Int32? useHlgt, System.Int32? useHlt, System.Int32? usePt, System.Int32? useLlt)
		{
			return AdverseEventMeddra_SearchTermsByKeyword(transactionManager, 0, int.MaxValue , searchTerm, useSoc, useHlgt, useHlt, usePt, useLlt);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_SearchTermsByKeyword' stored procedure. 
		/// </summary>
		/// <param name="searchTerm"> A <c>System.String</c> instance.</param>
		/// <param name="useSoc"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useHlgt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useHlt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="usePt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="useLlt"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventMeddra_SearchTermsByKeyword(TransactionManager transactionManager, int start, int pageLength , System.String searchTerm, System.Int32? useSoc, System.Int32? useHlgt, System.Int32? useHlt, System.Int32? usePt, System.Int32? useLlt);
		
		#endregion
		
		#region bbPharma_SAEMonthlyData 
		
		/// <summary>
		///	This method wrap the 'bbPharma_SAEMonthlyData' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_SAEMonthlyData()
		{
			return Pharma_SAEMonthlyData(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharma_SAEMonthlyData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_SAEMonthlyData(int start, int pageLength)
		{
			return Pharma_SAEMonthlyData(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbPharma_SAEMonthlyData' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Pharma_SAEMonthlyData(TransactionManager transactionManager)
		{
			return Pharma_SAEMonthlyData(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbPharma_SAEMonthlyData' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Pharma_SAEMonthlyData(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbCalculatePatientYears 
		
		/// <summary>
		///	This method wrap the 'bbCalculatePatientYears' stored procedure. 
		/// </summary>
		/// <param name="studyno"> A <c>System.String</c> instance.</param>
		/// <param name="mode"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="todayDate"> A <c>System.DateTime?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CalculatePatientYears(System.String studyno, System.Int32? mode, System.Int32? drugid, System.DateTime? todayDate)
		{
			return CalculatePatientYears(null, 0, int.MaxValue , studyno, mode, drugid, todayDate);
		}
		
		/// <summary>
		///	This method wrap the 'bbCalculatePatientYears' stored procedure. 
		/// </summary>
		/// <param name="studyno"> A <c>System.String</c> instance.</param>
		/// <param name="mode"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="todayDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CalculatePatientYears(int start, int pageLength, System.String studyno, System.Int32? mode, System.Int32? drugid, System.DateTime? todayDate)
		{
			return CalculatePatientYears(null, start, pageLength , studyno, mode, drugid, todayDate);
		}
				
		/// <summary>
		///	This method wrap the 'bbCalculatePatientYears' stored procedure. 
		/// </summary>
		/// <param name="studyno"> A <c>System.String</c> instance.</param>
		/// <param name="mode"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="todayDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet CalculatePatientYears(TransactionManager transactionManager, System.String studyno, System.Int32? mode, System.Int32? drugid, System.DateTime? todayDate)
		{
			return CalculatePatientYears(transactionManager, 0, int.MaxValue , studyno, mode, drugid, todayDate);
		}
		
		/// <summary>
		///	This method wrap the 'bbCalculatePatientYears' stored procedure. 
		/// </summary>
		/// <param name="studyno"> A <c>System.String</c> instance.</param>
		/// <param name="mode"> A <c>System.Int32?</c> instance.</param>
		/// <param name="drugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="todayDate"> A <c>System.DateTime?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet CalculatePatientYears(TransactionManager transactionManager, int start, int pageLength , System.String studyno, System.Int32? mode, System.Int32? drugid, System.DateTime? todayDate);
		
		#endregion
		
		#region bbDelegationLog_IsUserApprovedAtCentre 
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_IsUserApprovedAtCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_IsUserApprovedAtCentre(System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_IsUserApprovedAtCentre(null, 0, int.MaxValue , badbirUserId, centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_IsUserApprovedAtCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_IsUserApprovedAtCentre(int start, int pageLength, System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_IsUserApprovedAtCentre(null, start, pageLength , badbirUserId, centreId);
		}
				
		/// <summary>
		///	This method wrap the 'bbDelegationLog_IsUserApprovedAtCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet DelegationLog_IsUserApprovedAtCentre(TransactionManager transactionManager, System.Int32? badbirUserId, System.Int32? centreId)
		{
			return DelegationLog_IsUserApprovedAtCentre(transactionManager, 0, int.MaxValue , badbirUserId, centreId);
		}
		
		/// <summary>
		///	This method wrap the 'bbDelegationLog_IsUserApprovedAtCentre' stored procedure. 
		/// </summary>
		/// <param name="badbirUserId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="centreId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet DelegationLog_IsUserApprovedAtCentre(TransactionManager transactionManager, int start, int pageLength , System.Int32? badbirUserId, System.Int32? centreId);
		
		#endregion
		
		#region bbDruglkp_Get_List_Current 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Current' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Current()
		{
			return Druglkp_Get_List_Current(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Current' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Current(int start, int pageLength)
		{
			return Druglkp_Get_List_Current(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Current' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Current(TransactionManager transactionManager)
		{
			return Druglkp_Get_List_Current(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Current' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Current(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbPatientComorbidity_GetPagedCustom 
		
		/// <summary>
		///	This method wrap the 'bbPatientComorbidity_GetPagedCustom' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
			/// <param name="recordCount"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientComorbidity_GetPagedCustom(System.String whereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize, ref System.Int32? recordCount)
		{
			return PatientComorbidity_GetPagedCustom(null, 0, int.MaxValue , whereClause, orderBy, pageIndex, pageSize, ref recordCount);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientComorbidity_GetPagedCustom' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
			/// <param name="recordCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientComorbidity_GetPagedCustom(int start, int pageLength, System.String whereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize, ref System.Int32? recordCount)
		{
			return PatientComorbidity_GetPagedCustom(null, start, pageLength , whereClause, orderBy, pageIndex, pageSize, ref recordCount);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientComorbidity_GetPagedCustom' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
			/// <param name="recordCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PatientComorbidity_GetPagedCustom(TransactionManager transactionManager, System.String whereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize, ref System.Int32? recordCount)
		{
			return PatientComorbidity_GetPagedCustom(transactionManager, 0, int.MaxValue , whereClause, orderBy, pageIndex, pageSize, ref recordCount);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientComorbidity_GetPagedCustom' stored procedure. 
		/// </summary>
		/// <param name="whereClause"> A <c>System.String</c> instance.</param>
		/// <param name="orderBy"> A <c>System.String</c> instance.</param>
		/// <param name="pageIndex"> A <c>System.Int32?</c> instance.</param>
		/// <param name="pageSize"> A <c>System.Int32?</c> instance.</param>
			/// <param name="recordCount"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PatientComorbidity_GetPagedCustom(TransactionManager transactionManager, int start, int pageLength , System.String whereClause, System.String orderBy, System.Int32? pageIndex, System.Int32? pageSize, ref System.Int32? recordCount);
		
		#endregion
		
		#region bbDruglkp_Get_List_Conventional 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional()
		{
			return Druglkp_Get_List_Conventional(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional(int start, int pageLength)
		{
			return Druglkp_Get_List_Conventional(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional(TransactionManager transactionManager)
		{
			return Druglkp_Get_List_Conventional(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Conventional(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbSummaryPage_GetCurrentSystemicPsoriasisTherapy 
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetCurrentSystemicPsoriasisTherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetCurrentSystemicPsoriasisTherapy(System.Int32? patientid)
		{
			return SummaryPage_GetCurrentSystemicPsoriasisTherapy(null, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetCurrentSystemicPsoriasisTherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetCurrentSystemicPsoriasisTherapy(int start, int pageLength, System.Int32? patientid)
		{
			return SummaryPage_GetCurrentSystemicPsoriasisTherapy(null, start, pageLength , patientid);
		}
				
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetCurrentSystemicPsoriasisTherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet SummaryPage_GetCurrentSystemicPsoriasisTherapy(TransactionManager transactionManager, System.Int32? patientid)
		{
			return SummaryPage_GetCurrentSystemicPsoriasisTherapy(transactionManager, 0, int.MaxValue , patientid);
		}
		
		/// <summary>
		///	This method wrap the 'bbSummaryPage_GetCurrentSystemicPsoriasisTherapy' stored procedure. 
		/// </summary>
		/// <param name="patientid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet SummaryPage_GetCurrentSystemicPsoriasisTherapy(TransactionManager transactionManager, int start, int pageLength , System.Int32? patientid);
		
		#endregion
		
		#region bbPharmaLog_GetByFupAEID 
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByFupAEID' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByFupAEID(System.Int32? fupaeid)
		{
			return PharmaLog_GetByFupAEID(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByFupAEID' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByFupAEID(int start, int pageLength, System.Int32? fupaeid)
		{
			return PharmaLog_GetByFupAEID(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByFupAEID' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet PharmaLog_GetByFupAEID(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return PharmaLog_GetByFupAEID(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbPharmaLog_GetByFupAEID' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet PharmaLog_GetByFupAEID(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbAdverseEventMeddra_GetNamesByFupaeid 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_GetNamesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_GetNamesByFupaeid(System.Int32? fupaeid)
		{
			return AdverseEventMeddra_GetNamesByFupaeid(null, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_GetNamesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_GetNamesByFupaeid(int start, int pageLength, System.Int32? fupaeid)
		{
			return AdverseEventMeddra_GetNamesByFupaeid(null, start, pageLength , fupaeid);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_GetNamesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventMeddra_GetNamesByFupaeid(TransactionManager transactionManager, System.Int32? fupaeid)
		{
			return AdverseEventMeddra_GetNamesByFupaeid(transactionManager, 0, int.MaxValue , fupaeid);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventMeddra_GetNamesByFupaeid' stored procedure. 
		/// </summary>
		/// <param name="fupaeid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventMeddra_GetNamesByFupaeid(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupaeid);
		
		#endregion
		
		#region bbPatientdrug_GetByFupId_Concomitant 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Concomitant' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Concomitant(System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Concomitant(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Concomitant' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Concomitant(int start, int pageLength, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Concomitant(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Concomitant' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Concomitant(TransactionManager transactionManager, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Concomitant(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Concomitant' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public abstract TList<BbPatientdrug> Patientdrug_GetByFupId_Concomitant(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbNHSIC_Mark_All_Patients_Exported 
		
		/// <summary>
		///	This method wrap the 'bbNHSIC_Mark_All_Patients_Exported' stored procedure. 
		/// </summary>
		/// <param name="userName"> A <c>System.String</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet NHSIC_Mark_All_Patients_Exported(System.String userName)
		{
			return NHSIC_Mark_All_Patients_Exported(null, 0, int.MaxValue , userName);
		}
		
		/// <summary>
		///	This method wrap the 'bbNHSIC_Mark_All_Patients_Exported' stored procedure. 
		/// </summary>
		/// <param name="userName"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet NHSIC_Mark_All_Patients_Exported(int start, int pageLength, System.String userName)
		{
			return NHSIC_Mark_All_Patients_Exported(null, start, pageLength , userName);
		}
				
		/// <summary>
		///	This method wrap the 'bbNHSIC_Mark_All_Patients_Exported' stored procedure. 
		/// </summary>
		/// <param name="userName"> A <c>System.String</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet NHSIC_Mark_All_Patients_Exported(TransactionManager transactionManager, System.String userName)
		{
			return NHSIC_Mark_All_Patients_Exported(transactionManager, 0, int.MaxValue , userName);
		}
		
		/// <summary>
		///	This method wrap the 'bbNHSIC_Mark_All_Patients_Exported' stored procedure. 
		/// </summary>
		/// <param name="userName"> A <c>System.String</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet NHSIC_Mark_All_Patients_Exported(TransactionManager transactionManager, int start, int pageLength , System.String userName);
		
		#endregion
		
		#region bbAdverseEventFup_GetActiveByFupId_ClinicallyConfirmed 
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId_ClinicallyConfirmed' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId_ClinicallyConfirmed(System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId_ClinicallyConfirmed(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId_ClinicallyConfirmed' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId_ClinicallyConfirmed(int start, int pageLength, System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId_ClinicallyConfirmed(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId_ClinicallyConfirmed' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdverseEventFup_GetActiveByFupId_ClinicallyConfirmed(TransactionManager transactionManager, System.Int32? fupId)
		{
			return AdverseEventFup_GetActiveByFupId_ClinicallyConfirmed(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbAdverseEventFup_GetActiveByFupId_ClinicallyConfirmed' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdverseEventFup_GetActiveByFupId_ClinicallyConfirmed(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbNotification_GetSolved_ByInboxID 
		
		/// <summary>
		///	This method wrap the 'bbNotification_GetSolved_ByInboxID' stored procedure. 
		/// </summary>
		/// <param name="inboxId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Notification_GetSolved_ByInboxID(System.Int32? inboxId)
		{
			return Notification_GetSolved_ByInboxID(null, 0, int.MaxValue , inboxId);
		}
		
		/// <summary>
		///	This method wrap the 'bbNotification_GetSolved_ByInboxID' stored procedure. 
		/// </summary>
		/// <param name="inboxId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Notification_GetSolved_ByInboxID(int start, int pageLength, System.Int32? inboxId)
		{
			return Notification_GetSolved_ByInboxID(null, start, pageLength , inboxId);
		}
				
		/// <summary>
		///	This method wrap the 'bbNotification_GetSolved_ByInboxID' stored procedure. 
		/// </summary>
		/// <param name="inboxId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Notification_GetSolved_ByInboxID(TransactionManager transactionManager, System.Int32? inboxId)
		{
			return Notification_GetSolved_ByInboxID(transactionManager, 0, int.MaxValue , inboxId);
		}
		
		/// <summary>
		///	This method wrap the 'bbNotification_GetSolved_ByInboxID' stored procedure. 
		/// </summary>
		/// <param name="inboxId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Notification_GetSolved_ByInboxID(TransactionManager transactionManager, int start, int pageLength , System.Int32? inboxId);
		
		#endregion
		
		#region bbDruglkp_Get_List_Conventional_For_patdrugid_Baseline 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid_Baseline(System.Int32? patdrugid, System.Int32? fupid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid_Baseline(null, 0, int.MaxValue , patdrugid, fupid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid_Baseline(int start, int pageLength, System.Int32? patdrugid, System.Int32? fupid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid_Baseline(null, start, pageLength , patdrugid, fupid, isAdmin);
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Conventional_For_patdrugid_Baseline(TransactionManager transactionManager, System.Int32? patdrugid, System.Int32? fupid, System.Int32? isAdmin)
		{
			return Druglkp_Get_List_Conventional_For_patdrugid_Baseline(transactionManager, 0, int.MaxValue , patdrugid, fupid, isAdmin);
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Conventional_For_patdrugid_Baseline' stored procedure. 
		/// </summary>
		/// <param name="patdrugid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="fupid"> A <c>System.Int32?</c> instance.</param>
		/// <param name="isAdmin"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Conventional_For_patdrugid_Baseline(TransactionManager transactionManager, int start, int pageLength , System.Int32? patdrugid, System.Int32? fupid, System.Int32? isAdmin);
		
		#endregion
		
		#region bbCommittee_CentreWiseMonthlyRecruitmentBreakup 
		
		/// <summary>
		///	This method wrap the 'bbCommittee_CentreWiseMonthlyRecruitmentBreakup' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_CentreWiseMonthlyRecruitmentBreakup()
		{
			return Committee_CentreWiseMonthlyRecruitmentBreakup(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCommittee_CentreWiseMonthlyRecruitmentBreakup' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_CentreWiseMonthlyRecruitmentBreakup(int start, int pageLength)
		{
			return Committee_CentreWiseMonthlyRecruitmentBreakup(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbCommittee_CentreWiseMonthlyRecruitmentBreakup' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Committee_CentreWiseMonthlyRecruitmentBreakup(TransactionManager transactionManager)
		{
			return Committee_CentreWiseMonthlyRecruitmentBreakup(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbCommittee_CentreWiseMonthlyRecruitmentBreakup' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Committee_CentreWiseMonthlyRecruitmentBreakup(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbPatientdrug_GetByFupId_Previous 
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Previous' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Previous(System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Previous(null, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Previous' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Previous(int start, int pageLength, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Previous(null, start, pageLength , fupId);
		}
				
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Previous' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public TList<BbPatientdrug> Patientdrug_GetByFupId_Previous(TransactionManager transactionManager, System.Int32? fupId)
		{
			return Patientdrug_GetByFupId_Previous(transactionManager, 0, int.MaxValue , fupId);
		}
		
		/// <summary>
		///	This method wrap the 'bbPatientdrug_GetByFupId_Previous' stored procedure. 
		/// </summary>
		/// <param name="fupId"> A <c>System.Int32?</c> instance.</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="TList&lt;BbPatientdrug&gt;"/> instance.</returns>
		public abstract TList<BbPatientdrug> Patientdrug_GetByFupId_Previous(TransactionManager transactionManager, int start, int pageLength , System.Int32? fupId);
		
		#endregion
		
		#region bbDruglkp_Get_List_Biologic 
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic()
		{
			return Druglkp_Get_List_Biologic(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic(int start, int pageLength)
		{
			return Druglkp_Get_List_Biologic(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet Druglkp_Get_List_Biologic(TransactionManager transactionManager)
		{
			return Druglkp_Get_List_Biologic(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbDruglkp_Get_List_Biologic' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet Druglkp_Get_List_Biologic(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#region bbAdditionalUserDetail_getValidUsers 
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_getValidUsers' stored procedure. 
		/// </summary>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_getValidUsers()
		{
			return AdditionalUserDetail_getValidUsers(null, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_getValidUsers' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_getValidUsers(int start, int pageLength)
		{
			return AdditionalUserDetail_getValidUsers(null, start, pageLength );
		}
				
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_getValidUsers' stored procedure. 
		/// </summary>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public DataSet AdditionalUserDetail_getValidUsers(TransactionManager transactionManager)
		{
			return AdditionalUserDetail_getValidUsers(transactionManager, 0, int.MaxValue );
		}
		
		/// <summary>
		///	This method wrap the 'bbAdditionalUserDetail_getValidUsers' stored procedure. 
		/// </summary>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">Number of rows to return.</param>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <remark>This method is generate from a stored procedure.</remark>
		/// <returns>A <see cref="DataSet"/> instance.</returns>
		public abstract DataSet AdditionalUserDetail_getValidUsers(TransactionManager transactionManager, int start, int pageLength );
		
		#endregion
		
		#endregion

		#region Helper Functions	
		
		/// <summary>
		/// Fill a TList&lt;BbPatientdrug&gt; From a DataReader.
		/// </summary>
		/// <param name="reader">Datareader</param>
		/// <param name="rows">The collection to fill</param>
		/// <param name="start">Row number at which to start reading, the first row is 0.</param>
		/// <param name="pageLength">number of rows.</param>
		/// <returns>a <see cref="TList&lt;BbPatientdrug&gt;"/></returns>
		public static TList<BbPatientdrug> Fill(IDataReader reader, TList<BbPatientdrug> rows, int start, int pageLength)
		{
			NetTiersProvider currentProvider = DataRepository.Provider;
            bool useEntityFactory = currentProvider.UseEntityFactory;
            bool enableEntityTracking = currentProvider.EnableEntityTracking;
            LoadPolicy currentLoadPolicy = currentProvider.CurrentLoadPolicy;
			Type entityCreationFactoryType = currentProvider.EntityCreationalFactoryType;
			
			// advance to the starting row
			for (int i = 0; i < start; i++)
			{
				if (!reader.Read())
				return rows; // not enough rows, just return
			}
			for (int i = 0; i < pageLength; i++)
			{
				if (!reader.Read())
					break; // we are done
					
				string key = null;
				
				MHS.Badbir.NetTiers.Entities.BbPatientdrug c = null;
				if (useEntityFactory)
				{
					key = new System.Text.StringBuilder("BbPatientdrug")
					.Append("|").Append((int)reader[((int)BbPatientdrugColumn.Patdrugid - 1)]).ToString();
					c = EntityManager.LocateOrCreate<BbPatientdrug>(
					key.ToString(), // EntityTrackingKey
					"BbPatientdrug",  //Creational Type
					entityCreationFactoryType,  //Factory used to create entity
					enableEntityTracking); // Track this entity?
				}
				else
				{
					c = new MHS.Badbir.NetTiers.Entities.BbPatientdrug();
				}
				
				if (!enableEntityTracking ||
					c.EntityState == EntityState.Added ||
					(enableEntityTracking &&
					
						(
							(currentLoadPolicy == LoadPolicy.PreserveChanges && c.EntityState == EntityState.Unchanged) ||
							(currentLoadPolicy == LoadPolicy.DiscardChanges && c.EntityState != EntityState.Unchanged)
						)
					))
				{
					c.SuppressEntityEvents = true;
					c.Patdrugid = (int)reader[((int)BbPatientdrugColumn.Patdrugid - 1)];
					c.FupId = (int)reader[((int)BbPatientdrugColumn.FupId - 1)];
					c.Drugid = (int)reader[((int)BbPatientdrugColumn.Drugid - 1)];
					c.Startday = (reader.IsDBNull(((int)BbPatientdrugColumn.Startday - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Startday - 1)];
					c.Startmonth = (reader.IsDBNull(((int)BbPatientdrugColumn.Startmonth - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Startmonth - 1)];
					c.Startyear = (reader.IsDBNull(((int)BbPatientdrugColumn.Startyear - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Startyear - 1)];
					c.Startdate = (reader.IsDBNull(((int)BbPatientdrugColumn.Startdate - 1)))?null:(System.DateTime?)reader[((int)BbPatientdrugColumn.Startdate - 1)];
					c.Startestimated = (reader.IsDBNull(((int)BbPatientdrugColumn.Startestimated - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Startestimated - 1)];
					c.Stopday = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopday - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Stopday - 1)];
					c.Stopmonth = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopmonth - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Stopmonth - 1)];
					c.Stopyear = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopyear - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Stopyear - 1)];
					c.Stopdate = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopdate - 1)))?null:(System.DateTime?)reader[((int)BbPatientdrugColumn.Stopdate - 1)];
					c.Stopreasonid = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopreasonid - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Stopreasonid - 1)];
					c.Otherstopreason = (reader.IsDBNull(((int)BbPatientdrugColumn.Otherstopreason - 1)))?null:(string)reader[((int)BbPatientdrugColumn.Otherstopreason - 1)];
					c.Discontinued = (reader.IsDBNull(((int)BbPatientdrugColumn.Discontinued - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Discontinued - 1)];
					c.Dose = (reader.IsDBNull(((int)BbPatientdrugColumn.Dose - 1)))?null:(System.Double?)reader[((int)BbPatientdrugColumn.Dose - 1)];
					c.Doseunits = (reader.IsDBNull(((int)BbPatientdrugColumn.Doseunits - 1)))?null:(string)reader[((int)BbPatientdrugColumn.Doseunits - 1)];
					c.Doseunitid = (reader.IsDBNull(((int)BbPatientdrugColumn.Doseunitid - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Doseunitid - 1)];
					c.Frequency = (reader.IsDBNull(((int)BbPatientdrugColumn.Frequency - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Frequency - 1)];
					c.Commonfrequencyid = (reader.IsDBNull(((int)BbPatientdrugColumn.Commonfrequencyid - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Commonfrequencyid - 1)];
					c.Firstbiologic = (reader.IsDBNull(((int)BbPatientdrugColumn.Firstbiologic - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Firstbiologic - 1)];
					c.Systemic = (reader.IsDBNull(((int)BbPatientdrugColumn.Systemic - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Systemic - 1)];
					c.Inceightymgloadingdose = (reader.IsDBNull(((int)BbPatientdrugColumn.Inceightymgloadingdose - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Inceightymgloadingdose - 1)];
					c.Intermittentlyreceived = (reader.IsDBNull(((int)BbPatientdrugColumn.Intermittentlyreceived - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Intermittentlyreceived - 1)];
					c.Enteredasconventional = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredasconventional - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredasconventional - 1)];
					c.Enteredascurrent = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredascurrent - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredascurrent - 1)];
					c.Enteredasbiologic = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredasbiologic - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredasbiologic - 1)];
					c.Enteredaspast = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredaspast - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredaspast - 1)];
					c.Createdbyid = (int)reader[((int)BbPatientdrugColumn.Createdbyid - 1)];
					c.Createdbyname = (string)reader[((int)BbPatientdrugColumn.Createdbyname - 1)];
					c.Createddate = (System.DateTime)reader[((int)BbPatientdrugColumn.Createddate - 1)];
					c.Lastupdatedbyid = (int)reader[((int)BbPatientdrugColumn.Lastupdatedbyid - 1)];
					c.Lastupdatedbyname = (string)reader[((int)BbPatientdrugColumn.Lastupdatedbyname - 1)];
					c.Lastupdateddate = (System.DateTime)reader[((int)BbPatientdrugColumn.Lastupdateddate - 1)];
					c.DoseDateReason = (reader.IsDBNull(((int)BbPatientdrugColumn.DoseDateReason - 1)))?null:(string)reader[((int)BbPatientdrugColumn.DoseDateReason - 1)];
					c.DatesReconfirmed = (reader.IsDBNull(((int)BbPatientdrugColumn.DatesReconfirmed - 1)))?null:(System.Byte?)reader[((int)BbPatientdrugColumn.DatesReconfirmed - 1)];
					c.DosageReconfirmed = (reader.IsDBNull(((int)BbPatientdrugColumn.DosageReconfirmed - 1)))?null:(System.Byte?)reader[((int)BbPatientdrugColumn.DosageReconfirmed - 1)];
					c.GenericLoadingDose = (reader.IsDBNull(((int)BbPatientdrugColumn.GenericLoadingDose - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.GenericLoadingDose - 1)];
					c.Enteredassmallmolecule = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredassmallmolecule - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredassmallmolecule - 1)];
					c.AdminStopReasonId = (reader.IsDBNull(((int)BbPatientdrugColumn.AdminStopReasonId - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.AdminStopReasonId - 1)];
					c.StopReasonChecked = (reader.IsDBNull(((int)BbPatientdrugColumn.StopReasonChecked - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.StopReasonChecked - 1)];
					c.EntityTrackingKey = key;
					c.AcceptChanges();
					c.SuppressEntityEvents = false;
				}
				rows.Add(c);
			}
		return rows;
		}		
		/// <summary>
		/// Refreshes the <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> object from the <see cref="IDataReader"/>.
		/// </summary>
		/// <param name="reader">The <see cref="IDataReader"/> to read from.</param>
		/// <param name="entity">The <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> object to refresh.</param>
		public static void RefreshEntity(IDataReader reader, MHS.Badbir.NetTiers.Entities.BbPatientdrug entity)
		{
			if (!reader.Read()) return;
			
			entity.Patdrugid = (int)reader[((int)BbPatientdrugColumn.Patdrugid - 1)];
			entity.FupId = (int)reader[((int)BbPatientdrugColumn.FupId - 1)];
			entity.Drugid = (int)reader[((int)BbPatientdrugColumn.Drugid - 1)];
			entity.Startday = (reader.IsDBNull(((int)BbPatientdrugColumn.Startday - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Startday - 1)];
			entity.Startmonth = (reader.IsDBNull(((int)BbPatientdrugColumn.Startmonth - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Startmonth - 1)];
			entity.Startyear = (reader.IsDBNull(((int)BbPatientdrugColumn.Startyear - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Startyear - 1)];
			entity.Startdate = (reader.IsDBNull(((int)BbPatientdrugColumn.Startdate - 1)))?null:(System.DateTime?)reader[((int)BbPatientdrugColumn.Startdate - 1)];
			entity.Startestimated = (reader.IsDBNull(((int)BbPatientdrugColumn.Startestimated - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Startestimated - 1)];
			entity.Stopday = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopday - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Stopday - 1)];
			entity.Stopmonth = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopmonth - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Stopmonth - 1)];
			entity.Stopyear = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopyear - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Stopyear - 1)];
			entity.Stopdate = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopdate - 1)))?null:(System.DateTime?)reader[((int)BbPatientdrugColumn.Stopdate - 1)];
			entity.Stopreasonid = (reader.IsDBNull(((int)BbPatientdrugColumn.Stopreasonid - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Stopreasonid - 1)];
			entity.Otherstopreason = (reader.IsDBNull(((int)BbPatientdrugColumn.Otherstopreason - 1)))?null:(string)reader[((int)BbPatientdrugColumn.Otherstopreason - 1)];
			entity.Discontinued = (reader.IsDBNull(((int)BbPatientdrugColumn.Discontinued - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Discontinued - 1)];
			entity.Dose = (reader.IsDBNull(((int)BbPatientdrugColumn.Dose - 1)))?null:(System.Double?)reader[((int)BbPatientdrugColumn.Dose - 1)];
			entity.Doseunits = (reader.IsDBNull(((int)BbPatientdrugColumn.Doseunits - 1)))?null:(string)reader[((int)BbPatientdrugColumn.Doseunits - 1)];
			entity.Doseunitid = (reader.IsDBNull(((int)BbPatientdrugColumn.Doseunitid - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Doseunitid - 1)];
			entity.Frequency = (reader.IsDBNull(((int)BbPatientdrugColumn.Frequency - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Frequency - 1)];
			entity.Commonfrequencyid = (reader.IsDBNull(((int)BbPatientdrugColumn.Commonfrequencyid - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.Commonfrequencyid - 1)];
			entity.Firstbiologic = (reader.IsDBNull(((int)BbPatientdrugColumn.Firstbiologic - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Firstbiologic - 1)];
			entity.Systemic = (reader.IsDBNull(((int)BbPatientdrugColumn.Systemic - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Systemic - 1)];
			entity.Inceightymgloadingdose = (reader.IsDBNull(((int)BbPatientdrugColumn.Inceightymgloadingdose - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Inceightymgloadingdose - 1)];
			entity.Intermittentlyreceived = (reader.IsDBNull(((int)BbPatientdrugColumn.Intermittentlyreceived - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Intermittentlyreceived - 1)];
			entity.Enteredasconventional = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredasconventional - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredasconventional - 1)];
			entity.Enteredascurrent = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredascurrent - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredascurrent - 1)];
			entity.Enteredasbiologic = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredasbiologic - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredasbiologic - 1)];
			entity.Enteredaspast = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredaspast - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredaspast - 1)];
			entity.Createdbyid = (int)reader[((int)BbPatientdrugColumn.Createdbyid - 1)];
			entity.Createdbyname = (string)reader[((int)BbPatientdrugColumn.Createdbyname - 1)];
			entity.Createddate = (System.DateTime)reader[((int)BbPatientdrugColumn.Createddate - 1)];
			entity.Lastupdatedbyid = (int)reader[((int)BbPatientdrugColumn.Lastupdatedbyid - 1)];
			entity.Lastupdatedbyname = (string)reader[((int)BbPatientdrugColumn.Lastupdatedbyname - 1)];
			entity.Lastupdateddate = (System.DateTime)reader[((int)BbPatientdrugColumn.Lastupdateddate - 1)];
			entity.DoseDateReason = (reader.IsDBNull(((int)BbPatientdrugColumn.DoseDateReason - 1)))?null:(string)reader[((int)BbPatientdrugColumn.DoseDateReason - 1)];
			entity.DatesReconfirmed = (reader.IsDBNull(((int)BbPatientdrugColumn.DatesReconfirmed - 1)))?null:(System.Byte?)reader[((int)BbPatientdrugColumn.DatesReconfirmed - 1)];
			entity.DosageReconfirmed = (reader.IsDBNull(((int)BbPatientdrugColumn.DosageReconfirmed - 1)))?null:(System.Byte?)reader[((int)BbPatientdrugColumn.DosageReconfirmed - 1)];
			entity.GenericLoadingDose = (reader.IsDBNull(((int)BbPatientdrugColumn.GenericLoadingDose - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.GenericLoadingDose - 1)];
			entity.Enteredassmallmolecule = (reader.IsDBNull(((int)BbPatientdrugColumn.Enteredassmallmolecule - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.Enteredassmallmolecule - 1)];
			entity.AdminStopReasonId = (reader.IsDBNull(((int)BbPatientdrugColumn.AdminStopReasonId - 1)))?null:(System.Int32?)reader[((int)BbPatientdrugColumn.AdminStopReasonId - 1)];
			entity.StopReasonChecked = (reader.IsDBNull(((int)BbPatientdrugColumn.StopReasonChecked - 1)))?null:(System.Boolean?)reader[((int)BbPatientdrugColumn.StopReasonChecked - 1)];
			entity.AcceptChanges();
		}
		
		/// <summary>
		/// Refreshes the <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> object from the <see cref="DataSet"/>.
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> to read from.</param>
		/// <param name="entity">The <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> object.</param>
		public static void RefreshEntity(DataSet dataSet, MHS.Badbir.NetTiers.Entities.BbPatientdrug entity)
		{
			DataRow dataRow = dataSet.Tables[0].Rows[0];
			
			entity.Patdrugid = (int)dataRow["patdrugid"];
			entity.FupId = (int)dataRow["FupId"];
			entity.Drugid = (int)dataRow["drugid"];
			entity.Startday = Convert.IsDBNull(dataRow["startday"]) ? null : (System.Int32?)dataRow["startday"];
			entity.Startmonth = Convert.IsDBNull(dataRow["startmonth"]) ? null : (System.Int32?)dataRow["startmonth"];
			entity.Startyear = Convert.IsDBNull(dataRow["startyear"]) ? null : (System.Int32?)dataRow["startyear"];
			entity.Startdate = Convert.IsDBNull(dataRow["startdate"]) ? null : (System.DateTime?)dataRow["startdate"];
			entity.Startestimated = Convert.IsDBNull(dataRow["startestimated"]) ? null : (System.Boolean?)dataRow["startestimated"];
			entity.Stopday = Convert.IsDBNull(dataRow["stopday"]) ? null : (System.Int32?)dataRow["stopday"];
			entity.Stopmonth = Convert.IsDBNull(dataRow["stopmonth"]) ? null : (System.Int32?)dataRow["stopmonth"];
			entity.Stopyear = Convert.IsDBNull(dataRow["stopyear"]) ? null : (System.Int32?)dataRow["stopyear"];
			entity.Stopdate = Convert.IsDBNull(dataRow["stopdate"]) ? null : (System.DateTime?)dataRow["stopdate"];
			entity.Stopreasonid = Convert.IsDBNull(dataRow["stopreasonid"]) ? null : (System.Int32?)dataRow["stopreasonid"];
			entity.Otherstopreason = Convert.IsDBNull(dataRow["otherstopreason"]) ? null : (string)dataRow["otherstopreason"];
			entity.Discontinued = Convert.IsDBNull(dataRow["discontinued"]) ? null : (System.Int32?)dataRow["discontinued"];
			entity.Dose = Convert.IsDBNull(dataRow["dose"]) ? null : (System.Double?)dataRow["dose"];
			entity.Doseunits = Convert.IsDBNull(dataRow["doseunits"]) ? null : (string)dataRow["doseunits"];
			entity.Doseunitid = Convert.IsDBNull(dataRow["doseunitid"]) ? null : (System.Int32?)dataRow["doseunitid"];
			entity.Frequency = Convert.IsDBNull(dataRow["frequency"]) ? null : (System.Int32?)dataRow["frequency"];
			entity.Commonfrequencyid = Convert.IsDBNull(dataRow["commonfrequencyid"]) ? null : (System.Int32?)dataRow["commonfrequencyid"];
			entity.Firstbiologic = Convert.IsDBNull(dataRow["firstbiologic"]) ? null : (System.Boolean?)dataRow["firstbiologic"];
			entity.Systemic = Convert.IsDBNull(dataRow["systemic"]) ? null : (System.Boolean?)dataRow["systemic"];
			entity.Inceightymgloadingdose = Convert.IsDBNull(dataRow["inceightymgloadingdose"]) ? null : (System.Boolean?)dataRow["inceightymgloadingdose"];
			entity.Intermittentlyreceived = Convert.IsDBNull(dataRow["intermittentlyreceived"]) ? null : (System.Boolean?)dataRow["intermittentlyreceived"];
			entity.Enteredasconventional = Convert.IsDBNull(dataRow["enteredasconventional"]) ? null : (System.Boolean?)dataRow["enteredasconventional"];
			entity.Enteredascurrent = Convert.IsDBNull(dataRow["enteredascurrent"]) ? null : (System.Boolean?)dataRow["enteredascurrent"];
			entity.Enteredasbiologic = Convert.IsDBNull(dataRow["enteredasbiologic"]) ? null : (System.Boolean?)dataRow["enteredasbiologic"];
			entity.Enteredaspast = Convert.IsDBNull(dataRow["enteredaspast"]) ? null : (System.Boolean?)dataRow["enteredaspast"];
			entity.Createdbyid = (int)dataRow["createdbyid"];
			entity.Createdbyname = (string)dataRow["createdbyname"];
			entity.Createddate = (System.DateTime)dataRow["createddate"];
			entity.Lastupdatedbyid = (int)dataRow["lastupdatedbyid"];
			entity.Lastupdatedbyname = (string)dataRow["lastupdatedbyname"];
			entity.Lastupdateddate = (System.DateTime)dataRow["lastupdateddate"];
			entity.DoseDateReason = Convert.IsDBNull(dataRow["doseDateReason"]) ? null : (string)dataRow["doseDateReason"];
			entity.DatesReconfirmed = Convert.IsDBNull(dataRow["datesReconfirmed"]) ? null : (System.Byte?)dataRow["datesReconfirmed"];
			entity.DosageReconfirmed = Convert.IsDBNull(dataRow["dosageReconfirmed"]) ? null : (System.Byte?)dataRow["dosageReconfirmed"];
			entity.GenericLoadingDose = Convert.IsDBNull(dataRow["genericLoadingDose"]) ? null : (System.Int32?)dataRow["genericLoadingDose"];
			entity.Enteredassmallmolecule = Convert.IsDBNull(dataRow["enteredassmallmolecule"]) ? null : (System.Boolean?)dataRow["enteredassmallmolecule"];
			entity.AdminStopReasonId = Convert.IsDBNull(dataRow["adminStopReasonID"]) ? null : (System.Int32?)dataRow["adminStopReasonID"];
			entity.StopReasonChecked = Convert.IsDBNull(dataRow["stopReasonChecked"]) ? null : (System.Boolean?)dataRow["stopReasonChecked"];
			entity.AcceptChanges();
		}
		#endregion 
		
		#region DeepLoad Methods
		/// <summary>
		/// Deep Loads the <see cref="IEntity"/> object with criteria based of the child 
		/// property collections only N Levels Deep based on the <see cref="DeepLoadType"/>.
		/// </summary>
		/// <remarks>
		/// Use this method with caution as it is possible to DeepLoad with Recursion and traverse an entire object graph.
		/// </remarks>
		/// <param name="transactionManager"><see cref="TransactionManager"/> object</param>
		/// <param name="entity">The <see cref="MHS.Badbir.NetTiers.Entities.BbPatientdrug"/> object to load.</param>
		/// <param name="deep">Boolean. A flag that indicates whether to recursively save all Property Collection that are descendants of this instance. If True, saves the complete object graph below this object. If False, saves this object only. </param>
		/// <param name="deepLoadType">DeepLoadType Enumeration to Include/Exclude object property collections from Load.</param>
		/// <param name="childTypes">MHS.Badbir.NetTiers.Entities.BbPatientdrug Property Collection Type Array To Include or Exclude from Load</param>
		/// <param name="innerList">A collection of child types for easy access.</param>
	    /// <exception cref="ArgumentNullException">entity or childTypes is null.</exception>
	    /// <exception cref="ArgumentException">deepLoadType has invalid value.</exception>
		public override void DeepLoad(TransactionManager transactionManager, MHS.Badbir.NetTiers.Entities.BbPatientdrug entity, bool deep, DeepLoadType deepLoadType, System.Type[] childTypes, DeepSession innerList)
		{
			if(entity == null)
				return;

			#region CommonfrequencyidSource	
			if (CanDeepLoad(entity, "BbCommonFrequencylkp|CommonfrequencyidSource", deepLoadType, innerList) 
				&& entity.CommonfrequencyidSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = (entity.Commonfrequencyid ?? (int)0);
				BbCommonFrequencylkp tmpEntity = EntityManager.LocateEntity<BbCommonFrequencylkp>(EntityLocator.ConstructKeyFromPkItems(typeof(BbCommonFrequencylkp), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.CommonfrequencyidSource = tmpEntity;
				else
					entity.CommonfrequencyidSource = DataRepository.BbCommonFrequencylkpProvider.GetByCommonfrequencyid(transactionManager, (entity.Commonfrequencyid ?? (int)0));		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'CommonfrequencyidSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.CommonfrequencyidSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.BbCommonFrequencylkpProvider.DeepLoad(transactionManager, entity.CommonfrequencyidSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion CommonfrequencyidSource

			#region FupIdSource	
			if (CanDeepLoad(entity, "BbPatientCohortTracking|FupIdSource", deepLoadType, innerList) 
				&& entity.FupIdSource == null)
			{
				object[] pkItems = new object[1];
				pkItems[0] = entity.FupId;
				BbPatientCohortTracking tmpEntity = EntityManager.LocateEntity<BbPatientCohortTracking>(EntityLocator.ConstructKeyFromPkItems(typeof(BbPatientCohortTracking), pkItems), DataRepository.Provider.EnableEntityTracking);
				if (tmpEntity != null)
					entity.FupIdSource = tmpEntity;
				else
					entity.FupIdSource = DataRepository.BbPatientCohortTrackingProvider.GetByFupId(transactionManager, entity.FupId);		
				
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'FupIdSource' loaded. key " + entity.EntityTrackingKey);
				#endif 
				
				if (deep && entity.FupIdSource != null)
				{
					innerList.SkipChildren = true;
					DataRepository.BbPatientCohortTrackingProvider.DeepLoad(transactionManager, entity.FupIdSource, deep, deepLoadType, childTypes, innerList);
					innerList.SkipChildren = false;
				}
					
			}
			#endregion FupIdSource
			
			//used to hold DeepLoad method delegates and fire after all the local children have been loaded.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
			// Deep load child collections  - Call GetByPatdrugid methods when available
			
			#region BbPatientdrugdoseCollection
			//Relationship Type One : Many
			if (CanDeepLoad(entity, "List<BbPatientdrugdose>|BbPatientdrugdoseCollection", deepLoadType, innerList)) 
			{
				#if NETTIERS_DEBUG
				System.Diagnostics.Debug.WriteLine("- property 'BbPatientdrugdoseCollection' loaded. key " + entity.EntityTrackingKey);
				#endif 

				entity.BbPatientdrugdoseCollection = DataRepository.BbPatientdrugdoseProvider.GetByPatdrugid(transactionManager, entity.Patdrugid);

				if (deep && entity.BbPatientdrugdoseCollection.Count > 0)
				{
					deepHandles.Add("BbPatientdrugdoseCollection",
						new KeyValuePair<Delegate, object>((DeepLoadHandle<BbPatientdrugdose>) DataRepository.BbPatientdrugdoseProvider.DeepLoad,
						new object[] { transactionManager, entity.BbPatientdrugdoseCollection, deep, deepLoadType, childTypes, innerList }
					));
				}
			}		
			#endregion 
			
			
			//Fire all DeepLoad Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			deepHandles = null;
		}
		
		#endregion 
		
		#region DeepSave Methods

		/// <summary>
		/// Deep Save the entire object graph of the MHS.Badbir.NetTiers.Entities.BbPatientdrug object with criteria based of the child 
		/// Type property array and DeepSaveType.
		/// </summary>
		/// <param name="transactionManager">The transaction manager.</param>
		/// <param name="entity">MHS.Badbir.NetTiers.Entities.BbPatientdrug instance</param>
		/// <param name="deepSaveType">DeepSaveType Enumeration to Include/Exclude object property collections from Save.</param>
		/// <param name="childTypes">MHS.Badbir.NetTiers.Entities.BbPatientdrug Property Collection Type Array To Include or Exclude from Save</param>
		/// <param name="innerList">A Hashtable of child types for easy access.</param>
		public override bool DeepSave(TransactionManager transactionManager, MHS.Badbir.NetTiers.Entities.BbPatientdrug entity, DeepSaveType deepSaveType, System.Type[] childTypes, DeepSession innerList)
		{	
			if (entity == null)
				return false;
							
			#region Composite Parent Properties
			//Save Source Composite Properties, however, don't call deep save on them.  
			//So they only get saved a single level deep.
			
			#region CommonfrequencyidSource
			if (CanDeepSave(entity, "BbCommonFrequencylkp|CommonfrequencyidSource", deepSaveType, innerList) 
				&& entity.CommonfrequencyidSource != null)
			{
				DataRepository.BbCommonFrequencylkpProvider.Save(transactionManager, entity.CommonfrequencyidSource);
				entity.Commonfrequencyid = entity.CommonfrequencyidSource.Commonfrequencyid;
			}
			#endregion 
			
			#region FupIdSource
			if (CanDeepSave(entity, "BbPatientCohortTracking|FupIdSource", deepSaveType, innerList) 
				&& entity.FupIdSource != null)
			{
				DataRepository.BbPatientCohortTrackingProvider.Save(transactionManager, entity.FupIdSource);
				entity.FupId = entity.FupIdSource.FupId;
			}
			#endregion 
			#endregion Composite Parent Properties

			// Save Root Entity through Provider
			if (!entity.IsDeleted)
				this.Save(transactionManager, entity);
			
			//used to hold DeepSave method delegates and fire after all the local children have been saved.
			Dictionary<string, KeyValuePair<Delegate, object>> deepHandles = new Dictionary<string, KeyValuePair<Delegate, object>>();
	
			#region List<BbPatientdrugdose>
				if (CanDeepSave(entity.BbPatientdrugdoseCollection, "List<BbPatientdrugdose>|BbPatientdrugdoseCollection", deepSaveType, innerList)) 
				{	
					// update each child parent id with the real parent id (mostly used on insert)
					foreach(BbPatientdrugdose child in entity.BbPatientdrugdoseCollection)
					{
						if(child.PatdrugidSource != null)
						{
							child.Patdrugid = child.PatdrugidSource.Patdrugid;
						}
						else
						{
							child.Patdrugid = entity.Patdrugid;
						}

					}

					if (entity.BbPatientdrugdoseCollection.Count > 0 || entity.BbPatientdrugdoseCollection.DeletedItems.Count > 0)
					{
						//DataRepository.BbPatientdrugdoseProvider.Save(transactionManager, entity.BbPatientdrugdoseCollection);
						
						deepHandles.Add("BbPatientdrugdoseCollection",
						new KeyValuePair<Delegate, object>((DeepSaveHandle< BbPatientdrugdose >) DataRepository.BbPatientdrugdoseProvider.DeepSave,
							new object[] { transactionManager, entity.BbPatientdrugdoseCollection, deepSaveType, childTypes, innerList }
						));
					}
				} 
			#endregion 
				
			//Fire all DeepSave Items
			foreach(KeyValuePair<Delegate, object> pair in deepHandles.Values)
		    {
                pair.Key.DynamicInvoke((object[])pair.Value);
		    }
			
			// Save Root Entity through Provider, if not already saved in delete mode
			if (entity.IsDeleted)
				this.Save(transactionManager, entity);
				

			deepHandles = null;
						
			return true;
		}
		#endregion
	} // end class
	
	#region BbPatientdrugChildEntityTypes
	
	///<summary>
	/// Enumeration used to expose the different child entity types 
	/// for child properties in <c>MHS.Badbir.NetTiers.Entities.BbPatientdrug</c>
	///</summary>
	public enum BbPatientdrugChildEntityTypes
	{
		
		///<summary>
		/// Composite Property for <c>BbCommonFrequencylkp</c> at CommonfrequencyidSource
		///</summary>
		[ChildEntityType(typeof(BbCommonFrequencylkp))]
		BbCommonFrequencylkp,
		
		///<summary>
		/// Composite Property for <c>BbPatientCohortTracking</c> at FupIdSource
		///</summary>
		[ChildEntityType(typeof(BbPatientCohortTracking))]
		BbPatientCohortTracking,
		///<summary>
		/// Collection of <c>BbPatientdrug</c> as OneToMany for BbPatientdrugdoseCollection
		///</summary>
		[ChildEntityType(typeof(TList<BbPatientdrugdose>))]
		BbPatientdrugdoseCollection,
	}
	
	#endregion BbPatientdrugChildEntityTypes
	
	#region BbPatientdrugFilterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="SqlFilterBuilder&lt;BbPatientdrugColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrug"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugFilterBuilder : SqlFilterBuilder<BbPatientdrugColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugFilterBuilder class.
		/// </summary>
		public BbPatientdrugFilterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientdrugFilterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugFilterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientdrugFilterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientdrugFilterBuilder
	
	#region BbPatientdrugParameterBuilder
	
	/// <summary>
	/// A strongly-typed instance of the <see cref="ParameterizedSqlFilterBuilder&lt;BbPatientdrugColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrug"/> object.
	/// </summary>
	[CLSCompliant(true)]
	public class BbPatientdrugParameterBuilder : ParameterizedSqlFilterBuilder<BbPatientdrugColumn>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugParameterBuilder class.
		/// </summary>
		public BbPatientdrugParameterBuilder() : base() { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		public BbPatientdrugParameterBuilder(bool ignoreCase) : base(ignoreCase) { }

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugParameterBuilder class.
		/// </summary>
		/// <param name="ignoreCase">Specifies whether to create case-insensitive statements.</param>
		/// <param name="useAnd">Specifies whether to combine statements using AND or OR.</param>
		public BbPatientdrugParameterBuilder(bool ignoreCase, bool useAnd) : base(ignoreCase, useAnd) { }

		#endregion Constructors
	}

	#endregion BbPatientdrugParameterBuilder
	
	#region BbPatientdrugSortBuilder
    
    /// <summary>
    /// A strongly-typed instance of the <see cref="SqlSortBuilder&lt;BbPatientdrugColumn&gt;"/> class
	/// that is used exclusively with a <see cref="BbPatientdrug"/> object.
    /// </summary>
    [CLSCompliant(true)]
    public class BbPatientdrugSortBuilder : SqlSortBuilder<BbPatientdrugColumn>
    {
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the BbPatientdrugSqlSortBuilder class.
		/// </summary>
		public BbPatientdrugSortBuilder() : base() { }

		#endregion Constructors

    }    
    #endregion BbPatientdrugSortBuilder
	
} // end namespace
