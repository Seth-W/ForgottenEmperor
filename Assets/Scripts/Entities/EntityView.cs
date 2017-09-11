namespace CFE
{
    using UnityEngine;
    using System.Collections;

    class EntityView : MonoBehaviour
    {
        [SerializeField]
        Color color;

		Vector3 lastFramePos;

		Animator[] _anims;


		#region MonoBehaviours
        void Start()
        {
            //GetComponent<Renderer>().material.color = color;
			_anims = GetComponentsInChildren<Animator>();
			lastFramePos = transform.position;
        }

		void Update()
		{
			setAnimatorParams ("Horizontal Speed", transform.position.x - lastFramePos.x);
			setAnimatorParams ("Vertical Speed", transform.position.y - lastFramePos.y);
			lastFramePos = transform.position;
		}

		void OnEnable()
		{
			InputManager.PauseEvent += OnPauseEvent;
		}

		void OnDisable()
		{
			InputManager.PauseEvent -= OnPauseEvent;
		}

		#endregion

		#region EventResponders
		private void OnPauseEvent(bool pauseStatus)
		{
			int value = pauseStatus ? 0 : 1;
			foreach (Animator _anim in _anims)
			{
				_anim.speed = (float)value;
			}
		}
		#endregion

		/**
        *<summary>
        *Sets an integer animator parameter for this entity
        *</summary>
        */
		public void setAnimatorParams(string s, int i)
		{
			foreach (Animator _anim in _anims)
			{
				_anim.SetInteger (s, i);
			}
		}

		/**
        *<summary>
        *Sets a float animator parameter for this entity
        *</summary>
        */
		public void setAnimatorParams(string s, float f)
		{
			foreach (Animator _anim in _anims)
			{
				_anim.SetFloat (s, f);
			}
		}

		/**
        *<summary>
        *Sets a boolean animator parameter for this entity
        *</summary>
        */
		public void setAnimatorParams(string s, bool b)
		{
			foreach (Animator _anim in _anims)
			{
				_anim.SetBool(s, b);
			}
		}

		/**
        *<summary>
        *Activates a trigger animation parameter for this entity
        *</summary>
        */
		public void setAnimatorParams(string s)
		{
			foreach (Animator _anim in _anims)
			{
				_anim.SetTrigger (s);
			}
		}
    }
}