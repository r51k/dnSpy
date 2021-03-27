/*
    Copyright (C) 2014-2019 de4dot@gmail.com

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

namespace dnSpy.Contracts.Debugger.DotNet.Code {
	/// <summary>
	/// Method scope
	/// </summary>
	public sealed class DbgMethodDebugScope {
		/// <summary>
		/// Gets the span of this scope
		/// </summary>
		public DbgILSpan Span { get; }

		/// <summary>
		/// Gets all child scopes
		/// </summary>
		public DbgMethodDebugScope[] Scopes { get; }

		/// <summary>
		/// Gets all new locals in the scope
		/// </summary>
		public DbgLocal[] Locals { get; }

		/// <summary>
		/// Gets all new imports in the scope
		/// </summary>
		public DbgImportInfo[] Imports { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="span">Scope span</param>
		/// <param name="scopes">Child scopes</param>
		/// <param name="locals">Locals</param>
		/// <param name="imports">Imports</param>
		public DbgMethodDebugScope(DbgILSpan span, DbgMethodDebugScope[] scopes, DbgLocal[] locals, DbgImportInfo[] imports) {
			Span = span;
			Scopes = scopes ?? throw new ArgumentNullException(nameof(scopes));
			Locals = locals ?? throw new ArgumentNullException(nameof(locals));
			Imports = imports ?? throw new ArgumentNullException(nameof(imports));
		}
	}

	/// <summary>
	/// Import kind
	/// </summary>
	public enum DbgImportInfoKind {
		/// <summary>
		/// Namespace import
		/// </summary>
		Namespace,

		/// <summary>
		/// Type import
		/// </summary>
		Type,

		/// <summary>
		/// Namespace or type import
		/// </summary>
		NamespaceOrType,

		/// <summary>
		/// C#: extern alias
		/// </summary>
		Assembly,
	}

	/// <summary>
	/// Import info
	/// </summary>
	public readonly struct DbgImportInfo {
		/// <summary>
		/// Target kind
		/// </summary>
		public DbgImportInfoKind TargetKind { get; }

		/// <summary>
		/// Target
		/// </summary>
		public string? Target { get; }

		/// <summary>
		/// Alias
		/// </summary>
		public string? Alias { get; }

		/// <summary>
		/// Extern alias
		/// </summary>
		public string? ExternAlias { get; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="targetKind">Target kind</param>
		/// <param name="target">Target string</param>
		/// <param name="alias">Alias</param>
		/// <param name="externAlias">Extern alias</param>
		public DbgImportInfo(DbgImportInfoKind targetKind, string? target = null, string? alias = null, string? externAlias = null) {
			TargetKind = targetKind;
			Target = target;
			Alias = alias;
			ExternAlias = externAlias;
		}

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
		public static DbgImportInfo CreateNamespace(string @namespace) => new DbgImportInfo(DbgImportInfoKind.Namespace, target: @namespace);
		public static DbgImportInfo CreateNamespace(string @namespace, string externAlias) => new DbgImportInfo(DbgImportInfoKind.Namespace, target: @namespace, externAlias: externAlias);
		public static DbgImportInfo CreateType(string type) => new DbgImportInfo(DbgImportInfoKind.Type, target: type);
		public static DbgImportInfo CreateNamespaceAlias(string @namespace, string alias) => new DbgImportInfo(DbgImportInfoKind.Namespace, target: @namespace, alias: alias);
		public static DbgImportInfo CreateTypeAlias(string type, string alias) => new DbgImportInfo(DbgImportInfoKind.Type, target: type, alias: alias);
		public static DbgImportInfo CreateNamespaceAlias(string @namespace, string alias, string externAlias) => new DbgImportInfo(DbgImportInfoKind.Namespace, target: @namespace, alias: alias, externAlias: externAlias);
		public static DbgImportInfo CreateAssembly(string externAlias) => new DbgImportInfo(DbgImportInfoKind.Assembly, externAlias: externAlias);
		public static DbgImportInfo CreateAssembly(string externAlias, string assembly) => new DbgImportInfo(DbgImportInfoKind.Assembly, externAlias: externAlias, target: assembly);
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
	}
}
