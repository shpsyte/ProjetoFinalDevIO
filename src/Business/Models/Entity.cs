using System;

namespace Business.Models {
    public abstract class Entity {
        protected Entity () {
            Id = Guid.NewGuid ();
        }

        protected Guid Id { get; set; }

    }
}