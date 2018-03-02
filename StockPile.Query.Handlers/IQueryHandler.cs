using System;

namespace StockPile.Query.Handlers
{
    /// <summary>
    /// Simple interface to model a QueryHandler abstraction. 
    /// This can be used for inject query handlers dynamically into implementations, for example in Message or HTTP routing.
    /// </summary>
    /// <remarks>
    /// TODO: Extend the IQueryHandler with a second generic parameter for the expected return type
    /// e.g. IQueryHandler(Q, R) where Q : IQuery, R: IQueryResult
    ///     R handler.Handle(Q query);
    /// </remarks>
    /// <typeparam name="Q">
    /// Defines the query payload type that is passed to the handler. 
    /// </typeparam>
    public interface IQueryHandler<Q> where Q : IQuery
    {
        /// <summary>
        /// Handler method which can used to dynamically perform queries
        /// </summary>
        /// <param name="query"></param>
        /// <returns>The results of the query.</returns>
        object Handle(Q query);
    }
}
