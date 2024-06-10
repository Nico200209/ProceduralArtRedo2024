using UnityEngine;

namespace Demo {
	public class BuildTrigger : MonoBehaviour {
		public KeyCode BuildKey = KeyCode.Space;
		public bool BuildOnStart = false;
        public float DelayBeforeBuild = 5f;

        Shape Root;
		RandomGenerator rnd;

		void Start() {
			Root=GetComponent<Shape>();
			rnd=GetComponent<RandomGenerator>();

            if (BuildOnStart)
            {
				Debug.Log("I Was Called!");
                Invoke("Build", DelayBeforeBuild); // Invoke the Build method after the delay
            }
        }

		void Update() {
			if (Input.GetKeyDown(BuildKey)) {
				Build();
			}
		}

		void Build() {
			if (rnd!=null) {
				rnd.ResetRandom();
			}
			if (Root!=null) {
				Root.Generate();
			}
		}
	}
}