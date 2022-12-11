// ##################################################

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

// ##################################################

namespace Lugh.Logging
{
    public static class Trace
    {
        public static bool EnableWriteToFile { get; set; }

        private const string DebugTag = "[Debug] ";
        private const string InfoTag  = "[Info ] ";
        private const string ErrorTag = "[ERROR] ";

        private static string debugFilePath;
        private static string debugFileName;

        /// <summary>
        /// Write a debug string to logcat or console.
        /// The string can contain format options.
        /// </summary>
        /// <param name="message">The string to write.</param>
        /// <param name="args">Optional extra argumnts for use in format strings.</param>
        [SuppressMessage( "ReSharper", "InvalidXmlDocComment" )]
        public static void Dbg
            (
            [CallerFilePath]   string callerFilePath = "",
            [CallerMemberName] string callerMethod   = "",
            [CallerLineNumber] int    callerLine     = 0,
            string                    message        = null,
            params object[]           args
            )
        {
            if ( LughSystem.Inst().LogLevel == LughSystem.LogDebug )
            {
                message = string.Join( DebugTag, message );

                var callerID = new CallerID
                {
                    FileName   = Path.GetFileNameWithoutExtension( callerFilePath ),
                    MethodName = callerMethod,
                    LineNumber = callerLine
                };

                var str = CreateMessage( message, callerID, args );

                Console.WriteLine( str );

                WriteToFile( str );
            }
        }

        /// <summary>
        /// Writes a Debug message, but adds a divider line before and after the message.
        /// </summary>
        /// <param name="message">The string to write.</param>
        /// <param name="args">Optional extra argumnts for use in format strings.</param>
        [SuppressMessage( "ReSharper", "InvalidXmlDocComment" )]
        public static void BoxedDbg
            (
            [CallerFilePath]   string callerFilePath = "",
            [CallerMemberName] string callerMethod   = "",
            [CallerLineNumber] int    callerLine     = 0,
            string                    message        = "",
            params object[]           args
            )
        {
            if ( LughSystem.Inst().LogLevel == LughSystem.LogDebug )
            {
                Divider();

                message = string.Join( DebugTag, message );

                var callerID = new CallerID
                {
                    FileName   = Path.GetFileNameWithoutExtension( callerFilePath ),
                    MethodName = callerMethod,
                    LineNumber = callerLine
                };

                var str = CreateMessage( message, callerID, args );

                Console.WriteLine( str );

                WriteToFile( str );

                Divider();
            }
        }

        /// <summary>
        /// Write an error string to logcat or console.
        /// </summary>
        /// <param name="message">The string to write.</param>
        /// <param name="args">Optional extra argumnts for use in format strings.</param>
        [SuppressMessage( "ReSharper", "InvalidXmlDocComment" )]
        public static void Err
            (
            [CallerFilePath]   string callerFilePath = "",
            [CallerMemberName] string callerMethod   = "",
            [CallerLineNumber] int    callerLine     = 0,
            string                    message        = null,
            params object[]           args
            )
        {
            if ( LughSystem.Inst().LogLevel == LughSystem.LogDebug )
            {
                message = string.Join( ErrorTag, message );

                var callerID = new CallerID
                {
                    FileName   = Path.GetFileNameWithoutExtension( callerFilePath ),
                    MethodName = callerMethod,
                    LineNumber = callerLine
                };

                var str = CreateMessage( message, callerID, args );

                Console.WriteLine( str );

                WriteToFile( str );
            }
        }

        /// <summary>
        /// Write a message to logcat or console if the supplied condition is TRUE.
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        /// <param name="message">The string to write.</param>
        /// <param name="args">Optional extra argumnts for use in format strings.</param>
        [SuppressMessage( "ReSharper", "InvalidXmlDocComment" )]
        public static void _assert
            (
            [CallerFilePath]   string callerFilePath = "",
            [CallerMemberName] string callerMethod   = "",
            [CallerLineNumber] int    callerLine     = 0,
            bool                      condition      = false,
            string                    message        = "",
            params object[]           args
            )
        {
            if ( ( LughSystem.Inst().LogLevel == LughSystem.LogDebug ) && condition )
            {
                message = string.Join( DebugTag, message );

                var callerID = new CallerID
                {
                    FileName   = Path.GetFileNameWithoutExtension( callerFilePath ),
                    MethodName = callerMethod,
                    LineNumber = callerLine
                };

                var str = CreateMessage( message, callerID, args );

                Console.WriteLine( str );

                WriteToFile( str );
            }
        }

        /// <summary>
        /// Writes a debug message consisting solely of the following:-
        /// - Current time and date.
        /// - Calling Class/method/line number information.
        /// </summary>
        [SuppressMessage( "ReSharper", "InvalidXmlDocComment" )]
        public static void CheckPoint
            (
            [CallerFilePath]   string callerFilePath = "",
            [CallerMemberName] string callerMethod   = "",
            [CallerLineNumber] int    callerLine     = 0
            )
        {
            if ( LughSystem.Inst().LogLevel == LughSystem.LogDebug )
            {
                var sb = new StringBuilder();

                var callerID = new CallerID
                {
                    FileName   = Path.GetFileNameWithoutExtension( callerFilePath ),
                    MethodName = callerMethod,
                    LineNumber = callerLine
                };

                sb.Append( GetTimeStampInfo() );
                sb.Append( " : " );
                sb.Append( GetFileInfo( callerID ) );

                Console.WriteLine( sb.ToString() );

                WriteToFile( sb.ToString() );
            }
        }

        /// <summary>
        /// This method does the same as Trace#dbg(string, params object[] args)/>
        /// but does not output time and date information.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void Info( string message, params object[] args )
        {
            if ( LughSystem.Inst().LogLevel == LughSystem.LogDebug )
            {
                message = string.Join( InfoTag, message );

                var sb = new StringBuilder( message );

                if ( args.Length > 0 )
                {
                    foreach ( var arg in args )
                    {
                        sb.Append(' ');
                        sb.Append( arg );
                    }
                }

                message = string.Join( DebugTag, message );

                Console.WriteLine( message );

                WriteToFile( message );
            }
        }

        /// <summary>
        /// Creates a debug/error/info message ready for dumping.
        /// </summary>
        /// <param name="formatString">The base message</param>
        /// <param name="cid">Stack trace info from the calling method/file.</param>
        /// <param name="args">Optional additions to the message</param>
        /// <returns></returns>
        private static string CreateMessage
            (
            string          formatString,
            CallerID        cid,
            params object[] args
            )
        {
            var sb = new StringBuilder( GetTimeStampInfo() );

            sb.Append( " : " );
            sb.Append( GetFileInfo( cid ) );
            sb.Append( " : " );

            if ( !string.IsNullOrEmpty( formatString ) )
            {
                sb.Append( formatString );
            }

            if ( args.Length > 0 )
            {
                foreach ( var arg in args )
                {
                    sb.Append( ' ' );
                    sb.Append( arg );
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns a string holding the current time.
        /// </summary>
        private static string GetTimeStampInfo()
        {
            var c  = new GregorianCalendar();
            var sb = new StringBuilder();

            sb.Append( c.GetHour( DateTime.Now ) ).Append( ":" );
            sb.Append( c.GetMinute( DateTime.Now ) ).Append( ":" );
            sb.Append( c.GetSecond( DateTime.Now ) ).Append( ":" );
            sb.Append( c.GetMilliseconds( DateTime.Now ) );

            return sb.ToString();
        }

        /// <summary>
        /// Returns a string holding the calling filename, method and line number.
        /// </summary>
        private static string GetFileInfo( CallerID cid )
        {
            var sb = new StringBuilder();

            sb.Append( cid.FileName ).Append( "::" );
            sb.Append( cid.MethodName ).Append( "::" );
            sb.Append( cid.LineNumber );

            return sb.ToString();
        }

        public static void Divider( char ch = '-', int length = 80 )
        {
            var sb = new StringBuilder( DebugTag );

            for ( int i = 0; i < length; i++ )
            {
                sb.Append( ch );
            }

            Console.WriteLine( sb.ToString() );
        }

        /// <summary>
        /// Opens a physical file for writing copies of debug messages to.
        /// </summary>
        /// <param name="fileName">
        /// The filename. This should be filename only,
        /// and the file will be created in the working directory.
        /// </param>
        /// <param name="deleteExisting">
        /// True to delete existing copies of the file.
        /// False to append to existing file.
        /// </param>
        public static void OpenDebugFile( string fileName, bool deleteExisting )
        {
            if ( fileName != null )
            {
                if ( File.Exists( fileName ) && deleteExisting )
                {
                    File.Delete( fileName );
                }

                debugFilePath =  Environment.GetFolderPath( Environment.SpecialFolder.UserProfile );
                debugFilePath += "//.prefs//";
                debugFileName =  fileName;

                using ( FileStream fs = File.Create( debugFilePath + debugFileName ) )
                {
                    DateTime dateTime = DateTime.Now;

                    Byte[] divider =
                        new UTF8Encoding( true )
                            .GetBytes( "-----------------------------------------------------" );
                    Byte[] time = new UTF8Encoding( true ).GetBytes( dateTime.ToShortTimeString() );

                    fs.Write( divider, 0, divider.Length );
                    fs.Write( time,    0, time.Length );
                    fs.Write( divider, 0, divider.Length );

                    fs.Close();
                }
            }
        }

        /// <summary>
        /// Writes text to the logFile, if it exists.
        /// </summary>
        /// <param name="text">String holding the text to write.</param>
        private static void WriteToFile( string text )
        {
            if ( File.Exists( debugFilePath + debugFileName ) )
            {
                using var fs = File.Open( debugFilePath + debugFileName, FileMode.Append );

                var debugLine = new UTF8Encoding( true ).GetBytes( text + "\n" );
                fs.Write( debugLine, 0, debugLine.Length );
                    
                fs.Close();
            }
        }
    }
}