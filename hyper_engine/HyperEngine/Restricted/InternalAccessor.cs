using UnityEngine;

namespace HyperEngine.Restricted
{
    public abstract class InternalAccessor
    {
        protected internal static void SetHyperCoreTime(float time)
        {
            HyperCore.Time = time;
        }

        protected internal static void SetHyperCoreTotalTime(float time)
        {
            HyperCore.TotalTime = time;
        }

        protected internal static void InvokeFrameUpdate(HyperValues values)
        {
            HyperCore.UpdateFrame.Invoke(values);
        }

        protected internal static void PurgeAllUpdateListeners()
        {
            HyperCore.UpdateFrame.RemoveAllListeners();
        }

        protected internal static void SetUpPropertyManager(Transform content)
        {
            PropertyManager.SetUpPropertyManager(content);
        }
    }
}