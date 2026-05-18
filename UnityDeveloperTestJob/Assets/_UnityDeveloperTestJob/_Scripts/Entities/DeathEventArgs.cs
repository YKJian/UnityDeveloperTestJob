using System;

namespace Entities
{
    public class DeathEventArgs : EventArgs
	{
		public Entity deadEntity { get; }

		public DeathEventArgs(Entity entity)
		{
			deadEntity = entity;
		}
	}
}