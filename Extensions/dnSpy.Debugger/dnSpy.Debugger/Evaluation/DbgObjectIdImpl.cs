﻿/*
    Copyright (C) 2014-2017 de4dot@gmail.com

    This file is part of dnSpy

    dnSpy is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    dnSpy is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with dnSpy.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using dnSpy.Contracts.Debugger;
using dnSpy.Contracts.Debugger.Evaluation;
using dnSpy.Contracts.Debugger.Evaluation.Engine;

namespace dnSpy.Debugger.Evaluation {
	sealed class DbgObjectIdImpl : DbgObjectId {
		public override DbgRuntime Runtime => owner.Runtime;
		public override uint Id => EngineObjectId.Id;
		public DbgEngineObjectId EngineObjectId { get; }

		readonly DbgRuntimeObjectIdServiceImpl owner;

		public DbgObjectIdImpl(DbgRuntimeObjectIdServiceImpl owner, DbgEngineObjectId engineObjectId) {
			this.owner = owner ?? throw new ArgumentNullException(nameof(owner));
			EngineObjectId = engineObjectId ?? throw new ArgumentNullException(nameof(engineObjectId));
		}

		public override DbgValue GetValue() => new DbgValueImpl(Runtime, EngineObjectId.GetValue());
		public override void Remove() => owner.Remove(new[] { this });
		protected override void CloseCore() => EngineObjectId.Close(Process.DbgManager.Dispatcher);
	}
}