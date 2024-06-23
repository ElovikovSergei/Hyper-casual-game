using System;

namespace Core.Entities
{
    public abstract class Person : Entity
    {
        public float MoveSpeed { get; private set; }
        public float RotateSpeed { get; private set; }

        public Person(float moveSpeed, float rotateSpeed, Guid id) : base(id)
        {
            MoveSpeed = moveSpeed;
            RotateSpeed = rotateSpeed;
        }
    }
}