﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Suplex.Utilities
{
    public class AssemblyLoader
    {
        static HashSet<string> _knownFiles = null;

        public static T Load<T>(string type, string defaultType) where T : class
        {
            T obj = null;

            if( _knownFiles == null )
            {
                _knownFiles = new HashSet<string>();
                _knownFiles.Add( "log4net" );
                _knownFiles.Add( "microsoft.visualstudio.qualitytools.unittestframework" );
                _knownFiles.Add( "nunit.framework" );
                _knownFiles.Add( "sqlite.interop" );
                _knownFiles.Add( "synapse.service.common" );
                _knownFiles.Add( "synapse.unittests" );
                _knownFiles.Add( "system.data.sqlite" );
                _knownFiles.Add( "yamldotnet" );
            }

            if( string.IsNullOrWhiteSpace( type ) )
                type = defaultType;

            if( type.Contains( ":" ) )
            {
                string[] typeInfo = type.Split( ':' );
                AssemblyName an = new AssemblyName( typeInfo[0] );
                Assembly hrAsm = Assembly.Load( an );

                Type t = hrAsm.GetType( typeInfo[1], true );
                obj = Activator.CreateInstance( t ) as T;
            }
            else
            {
                string currentDir = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location );
                DirectoryInfo dirInfo = new DirectoryInfo( currentDir );
                List<FileInfo> files = new List<FileInfo>(
                    dirInfo.EnumerateFiles( "*.dll", SearchOption.AllDirectories ) );

                IEnumerator<FileInfo> fileList = files.GetEnumerator();
                while( fileList.MoveNext() && obj == null )
                {
                    string current = fileList.Current.Name.ToLower().Replace( fileList.Current.Extension.ToLower(), string.Empty );
                    if( !_knownFiles.Contains( current ) )
                    {
                        //assume that the name is complete, including namespace (if there is one)
                        try
                        {
                            AssemblyName an = new AssemblyName( current );
                            Assembly hrAsm = Assembly.Load( an );

                            Type t = hrAsm.GetType( type, true );
                            obj = Activator.CreateInstance( t ) as T;
                        }
                        catch
                        {
                            //probe all the Types, looking for partial match in name
                            try
                            {
                                AssemblyName an = new AssemblyName( current );
                                Assembly hrAsm = Assembly.Load( an );

                                string tl = type.ToLower();
                                Type[] types = hrAsm.GetTypes();
                                foreach( Type t in types )
                                    if( t.GetInterfaces().Contains( typeof( T ) ) && t.Name.ToLower().Contains( tl ) )
                                    {
                                        obj = Activator.CreateInstance( t ) as T;
                                        break;
                                    }
                            }
                            catch { }
                        }
                    }
                }
            }

            return obj;
        }
    }
}