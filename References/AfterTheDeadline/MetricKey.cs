using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AfterTheDeadline
{
    public enum MetricKey
    {
        #region Grammar Keys

        /// <summary>
        /// number of grammar errors
        /// </summary>
        Errors,

        /// <summary>
        /// number of miscellaneous things to revise
        /// </summary>
        Revise,

        /// <summary>
        /// number of times "the the" or some other repeated sequence occurs
        /// </summary>
        RepeatedWords,

        #endregion Grammer Keys

        #region Spell Keys

        /// <summary>
        /// number of phrases found missing a hypen
        /// </summary>
        Hyphenate,

        /// <summary>
        /// number of misused words detected
        /// </summary>
        MisusedWords,

        /// <summary>
        /// raw number of words not in AtD's dictionary
        /// </summary>
        Raw,

        /// <summary>
        /// number of times a known common misspelling occurs (these are almost always a legit error)
        /// </summary>
        Estimate,

        #endregion Spell Keys

        #region Stats Keys

        /// <summary>
        /// number of sentences in the document (estimate)
        /// </summary>
        Sentences,

        /// <summary>
        /// number of words in the document
        /// </summary>
        Words,

        #endregion Stats Keys

        #region Style Keys

        /// <summary>
        /// number of cliche phrases found
        /// </summary>
        Cliches,

        /// <summary>
        /// number of complex phrases found
        /// </summary>
        ComplexPhrases,

        /// <summary>
        /// number of hidden verb phrases found
        /// </summary>
        HiddenVerbs,

        /// <summary>
        /// number of passive phrases found
        /// </summary>
        PassiveVoice

        #endregion Style Keys
    }
}
