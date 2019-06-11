using System;
using System.Collections.Generic;

using Agathas.Storefront.Infrastructure.Querying;

using NHibernate;
using NHibernate.Criterion;

namespace Agathas.Storefront.Repository.NHibernate.Repositories {
  public static class QueryTranslator {
    public static ICriteria TranslateIntoNHQuery<T>(this Query query, ICriteria criteria) {
      BuildQueryFrom(query, criteria);

      if (query.OrderByProperty != null)
        criteria.AddOrder(
          new Order(query.OrderByProperty.PropertyName, !query.OrderByProperty.Desc));

      return criteria;
    }

    private static void BuildQueryFrom(Query query, ICriteria criteria) {
      IList<ICriterion> critrions = new List<ICriterion>();

      if (query.Criteria != null) {
        foreach (var c in query.Criteria) {
          ICriterion criterion;

          switch (c.criteriaOperator) {
            case CriteriaOperator.Equal:
              criterion = Expression.Eq(c.PropertyName, c.Value);
              break;
            case CriteriaOperator.LesserThanOrEqual:
              criterion = Expression.Le(c.PropertyName, c.Value);
              break;
            default:
              throw new ApplicationException("No operator defined");
          }

          critrions.Add(criterion);
        }

        if (query.QueryOperator == QueryOperator.And) {
          var andSubQuery = Expression.Conjunction();
          foreach (var criterion in critrions) {
            andSubQuery.Add(criterion);
          }

          criteria.Add(andSubQuery);
        } else {
          Disjunction orSubQuery = Expression.Disjunction();
          foreach (var criterion in critrions) {
            orSubQuery.Add(criterion);
          }
          criteria.Add(orSubQuery);
        }

        foreach (var sub in query.SubQueries) {
          BuildQueryFrom(sub, criteria);
        }
      }
    }
  }
}
