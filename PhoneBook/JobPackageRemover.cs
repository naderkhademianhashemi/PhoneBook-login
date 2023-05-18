using Optimate.Bussiness.DoWorkable;
using Optimate.Entity;
using System.Data.Common;

namespace Optimate.BaseDataBL
{
    public class JobPackageRemover : BussinessBase<int, Base_JobDictionaryView>
    {
        private int JobID => Data;

        public JobPackageRemover(int JobID) : base(JobID, ActionEnum101.حذف_شغل_مشتری)
        {
        }
        protected async override Task<Base_JobDictionaryView> DoWorkAsync(DbTransaction Transaction)
        {
            var TmpJobBL = new Base_JobBL();
            var TmpJob = await TmpJobBL.FoundWithKeyFieldValueAsync(JobID,Transaction);
            if (TmpJob.IsNullOrEmpty())
                throw ExceptionEnum101.Exception_JobdNotFound.GetHTSException();
            TmpJob.Deleted = true;
            var DeletedJob = await TmpJobBL.UpdateAsync(TmpJob, true, Transaction);
            var TmpJobDictionary = new Base_JobDictionaryViewBL();
            return await TmpJobDictionary.FirstOrDefaultAsync(q => q.ID == DeletedJob.ID,Transaction);
        }
    }
}
