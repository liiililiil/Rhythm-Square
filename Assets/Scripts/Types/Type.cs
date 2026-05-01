using System;
using AudioManagement;
using Types.Utils;
using UnityEngine;

namespace Type
{

    public class PlayableMusicReceiver
    {
        public PlayableMusicReceiver(Action<PlayableMusic> onLoadPlayableMusic, Action<MusicInfo> onLoadMusicInfo)
        {
            PlayableMusicSender.Instance.onLoadMusicInfo.AddListener(onLoadMusicInfo);
            PlayableMusicSender.Instance.onLoadPlayerableMusic.AddListener(onLoadPlayableMusic);
        }
    }

    public struct Vector2Byte : IEquatable<Vector2Byte>
    {
        private static readonly Vector2Byte zeroVector = new Vector2Byte(0, 0);
        public byte x { get; set; }
        public byte y { get; set; }

        public Vector2Byte(byte x, byte y)
        {
            this.x = x;
            this.y = y;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(x, y);
        }

        //
        public bool Equals(Vector2Byte other)
        {
            return x == other.x && y == other.y;
        }
        public override bool Equals(object obj)
        {
            return obj is Vector2Byte other && Equals(other);
        }


        public static implicit operator Vector2Byte(Vector2 target)
        {
            return new Vector2Byte((byte)target.x, (byte)target.y);
        }

        public static implicit operator Vector2Byte(Vector2Int target)
        {
            return new Vector2Byte((byte)target.x, (byte)target.y);
        }

        public static implicit operator Vector2(Vector2Byte target)
        {
            return new Vector2(target.x, target.y);
        }

        public static bool operator ==(Vector2Byte a, Vector2Byte b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Vector2Byte a, Vector2Byte b)
        {
            return !a.Equals(b);
        }

        public static Vector2Byte zero => zeroVector;
    }

    [Serializable]
    public class MusicPart
    {
        public FloatRange startAt;
        public FloatRange loop;
        public FloatRange endAt;
    }
}