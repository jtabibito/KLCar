using UnityEngine;
using System.Collections;

/**
 * 可以播放的对象.并且可以设定存活时间.
 */
public class PlayAble : MonoBehaviour
{
		/// <summary>
		/// 是否自动启动播放.
		/// </summary>
		public bool autoPlay=true;
		/**
		 * 存活时间.
		 */
		public float duration;
		/**
		 * 当超过存活时间后,是自动销毁还是自动隐藏.
		 */
		public bool autoDestory;
		/**
		 * 但重复调用播放时,是否打断原来的播放.
		 */
		public bool breakOnPlay;
		/**
		 * 剩余的存活时间.
		 */
		private float totalDuration;
		private bool _isPlay;
		/// <summary>
		/// 当前对象是不是一个例子发射器.如果是,则只是播放粒子效果.
		/// </summary>
		public bool isParticleEmitter;

		public static PlayAble AddPlayAbleTo (GameObject target, float duration, bool autoDestory)
		{	
				PlayAble pa = target.AddComponent<PlayAble> ();
				pa.duration = duration;
				pa.autoDestory = autoDestory;
				return pa;
		}

		void Start ()
		{
			if (autoPlay) {
				play ();
			}
		}
		
		void Update ()
		{
			 	if (totalDuration > 0) {
						totalDuration -= Time.deltaTime;
						if (totalDuration <= 0) {
								stop ();
						}
				}
		}

		public void play ()
		{
				if (breakOnPlay || !_isPlay) {
						onPlay ();
				} else if (_isPlay) {
						totalDuration = duration;
				}
		}

		public void stop ()
		{
				if (_isPlay) {
						onStop ();
				}
		}

		public bool isPlay {
				get {
						return _isPlay;
				}
		}
		/**
		 * 开始播放的事件.
		 */
		protected virtual void onPlay ()
		{
				_isPlay = true;
				if (duration != 0) {
						totalDuration = duration;
				}
				if (!gameObject.activeSelf) {
						gameObject.SetActive (true);
				}
				if (isParticleEmitter && particleEmitter != null) {
						particleEmitter.emit = true;
				}
		}
		/**
		 * 播放停止的事件.
		 */
		protected virtual void onStop ()
		{
				_isPlay = false;
				totalDuration = 0;
				onPlayOver ();
		}
		/**
		 * 播放停止后.需要销毁或者结束播放时的事件.
		 */
		protected virtual void onPlayOver ()
		{
				if (isParticleEmitter) {
						if (particleEmitter != null) {
								particleEmitter.emit = false;
						}
				} else {
						if (autoDestory) {
								Destroy (gameObject);
						} else {
								gameObject.SetActive (false);
						}
				}
		}
}
