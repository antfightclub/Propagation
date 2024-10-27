using System.Collections.Generic;
using UnityEngine;

namespace SpecialRelativity
{
    public class CelestialObjectController : MonoBehaviour
    {
        private static readonly HashSet<CelestialObjectController> _instances = new HashSet<CelestialObjectController>();

        public static HashSet<CelestialObjectController> Instances => new HashSet<CelestialObjectController>(_instances);

        [SerializeField] private Transform trans;

        public Transform Trans => trans;

        private void Awake()
        {
            if(!trans) trans = GetComponent<Transform>();
            _instances.Add(this);
        }

        private void OnDestroy()
        {
            _instances.Remove(this);
        }

    }

}
