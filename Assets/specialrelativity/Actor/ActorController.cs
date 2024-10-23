using System.Collections.Generic;
using UnityEngine;

namespace SpecialRelativity.Entity
{
    public class ActorController : MonoBehaviour
    {
        private static readonly HashSet<ActorController> _instances = new HashSet<ActorController>();

        public static HashSet<ActorController> Instances => new HashSet<ActorController>(_instances);

        [SerializeField] private Transform trans;

        public Transform Trans => trans; // actors use Unity transform but I may have to use a self-defined type for keeping track of stuff in double space

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
