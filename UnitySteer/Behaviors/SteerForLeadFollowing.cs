#define ANNOTATE_PURSUIT
using UnityEngine;

namespace UnitySteer.Behaviors
{
    /// <summary>
    /// Steers a vehicle to pursue another one
    /// </summary>
    [AddComponentMenu("UnitySteer/Steer/... for Lead Following")]
    public class SteerForLeadFollowing : Steering
    {

        /// <summary>
        /// Target point
        /// </summary>
        /// <remarks>
        /// Declared as a separate value so that we can inspect it on Unity in 
        /// debug mode.
        /// </remarks>
        [SerializeField]
        private DetectableObject _quarry;

        /// <summary>
        /// Should the vehicle's velocity be considered in the seek calculations?
        /// </summary>
        /// <remarks>
        /// If true, the vehicle will slow down as it approaches its target. See
        /// the remarks on GetSeekVector.
        /// </remarks>
        [SerializeField]
        private bool _considerVelocity;

        /// <summary>
        /// The offset between it and the leader.
        /// </summary>
        [SerializeField]
        private Vector3 _offset = Vector3.zero;


        /// <summary>
        /// The target object.
        /// </summary>
        public DetectableObject Quarry
        {
            get { return _quarry; }
            set
            {
                if (_quarry != value)
                {
                    ReportedArrival = false;
                    _quarry = value;
                }
            }
        }

        /// <summary>
        /// The offset.
        /// </summary>
        public Vector3 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }


        /// <summary>
        /// Should the vehicle's velocity be considered in the seek calculations?
        /// </summary>
        /// <remarks>
        /// If true, the vehicle will slow down as it approaches its target
        /// </remarks>
        public bool ConsiderVelocity
        {
            get { return _considerVelocity; }
            set { _considerVelocity = value; }
        }

        //protected override void Start()
        //{
        //    base.Start();

        //    if (_defaultToCurrentPosition && TargetPoint == Vector3.zero)
        //    {
        //        enabled = false;
        //    }
        //}

        /// <summary>
        /// Calculates the force to apply to the vehicle to reach a point
        /// </summary>
        /// <returns>
        /// A <see cref="Vector3"/>
        /// </returns>
        
        protected override Vector3 CalculateForce()
        {
            return Vehicle.GetSeekVector(_quarry.transform.position+_offset.z*_quarry.transform.forward
                +_offset.x*_quarry.transform.right, _considerVelocity);
            //return Vehicle.GetSeekVector(_quarry.transform.position + _offset, _considerVelocity);
        }
    }
}