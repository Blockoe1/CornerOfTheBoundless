/*****************************************************************************
// File Name : SlowmodeController.cs
// Author : Eli Koederitz
// Creation Date : 12/30/2025
// Last Modified : 12/30/2025
//
// Brief Description : Controls tracking a character's slowmode and when they can act.
*****************************************************************************/
using UnityEngine;
using UnityEngine.Events;

namespace COTB.Combat
{
    public class SlowmodeController : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnSlowmodeEnd;

        private int slowmode;
        /// <summary>
        /// Controlls sequencing individual combatant actions.
        /// </summary>
        private static CombatSequencer sequencer;

        #region Properties
        public int Slowmode
        {
            get
            {
                return slowmode;
            }
            private set
            {
                slowmode = Mathf.Max(value, 0);
                if (slowmode == 0)
                {
                    OnSlowmodeEnd?.Invoke();
                }
            }
        }
        private static CombatSequencer Sequencer
        {
            get
            {
                if (sequencer == null) { sequencer = new CombatSequencer(); }
                return sequencer;
            }
            set { sequencer = value; }
        }
        #endregion

        /// <summary>
        /// Registers this SlowmodeController with the SlowmodeCoordinator
        /// </summary>
        private void Awake()
        {
            Sequencer.RegisterController(this);
        }
        private void OnDestroy()
        {
            Sequencer.UnregisterController(this);
            if (Sequencer.FLagNull)
            {
                Sequencer = null;
            }
        }

        /// <summary>
        /// Tick's down this character's slowmode after each action.
        /// </summary>
        public void UpdateSlowmode()
        {
            if (Slowmode > 0)
            {
                Slowmode -= 1;
            }
        }
    }
}
