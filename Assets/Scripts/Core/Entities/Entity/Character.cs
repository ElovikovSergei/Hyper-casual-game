using System;

namespace Core.Entities
{
    public sealed class Character : Person
    {
        public Character(float moveSpeed, float rotateSpeed, Guid id) : base(moveSpeed, rotateSpeed, id) { }
    }
}