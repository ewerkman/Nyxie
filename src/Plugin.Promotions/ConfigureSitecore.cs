﻿using Microsoft.Extensions.DependencyInjection;
using Promethium.Plugin.Promotions.Pipelines.Blocks;
using Promethium.Plugin.Promotions.Resolvers;
using Sitecore.Commerce.Core;
using Sitecore.Framework.Configuration;
using Sitecore.Framework.Pipelines.Definitions.Extensions;
using Sitecore.Framework.Rules;
using System.Reflection;

namespace Promethium.Plugin.Promotions
{
    /// <summary>
    /// The configure sitecore class.
    /// </summary>
    public class ConfigureSitecore : IConfigureSitecore
    {
        /// <summary>
        /// The configure services.
        /// </summary>
        /// <param name="services">
        /// The services.
        /// </param>
        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.RegisterAllPipelineBlocks(assembly);
            services.RegisterAllCommands(assembly);

            services.AddTransient<CategoryPathResolver>();
            services.AddTransient<CategoryCartLinesResolver>();
            services.AddTransient<CategoryOrderLinesResolver>();
            services.AddTransient<OrderResolver>();

            services.Sitecore().Rules(rules => rules.Registry(reg => reg.RegisterThisAssembly()));

            services.Sitecore().Pipelines(config => config
                .ConfigurePipeline<Sitecore.Commerce.Plugin.Carts.IAddCartLinePipeline>(configure => configure
                    .Add<Pipelines.Blocks.AddCategoryBlock>()
                    .Before<Sitecore.Commerce.Plugin.Carts.PersistCartBlock>())

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_CategoryBlock>()
                    .After<Sitecore.Commerce.Plugin.Catalog.GetPromotionQualificationDetailsViewBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_FulfillmentBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_CategoryBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_PaymentBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_FulfillmentBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_BasicStringCompareBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_PaymentBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_ApplyActionTo>()
                    .After<Pipelines.Blocks.ConditionDetailsView_BasicStringCompareBlock>())

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.PrettifyPromotionChildrenDetailsBlock>()
                    .After<Sitecore.Commerce.EntityViews.IFormatEntityViewPipeline>())

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<Sitecore.Commerce.Plugin.Search.ISearchPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ExtendCategorySearchResultBlock>()
                    .After<Sitecore.Commerce.EntityViews.IFormatEntityViewPipeline>())

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_CategoryBlock>()
                    .After<Sitecore.Commerce.Plugin.Catalog.DoActionSelectQualificationBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_FulfillmentBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_CategoryBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_PaymentBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_FulfillmentBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_BasicStringCompareBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_PaymentBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_ApplyActionTo>()
                    .After<Pipelines.Blocks.ConditionDetailsView_BasicStringCompareBlock>())

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_FulfillmentBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_CategoryBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_PaymentBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_FulfillmentBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IGetEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_BasicStringCompareBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_PaymentBlock>())

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_CategoryBlock>()
                    .After<Sitecore.Commerce.Plugin.Promotions.DoActionSelectQualificationBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_FulfillmentBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_CategoryBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_PaymentBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_FulfillmentBlock>())

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IDoActionPipeline>(configure => configure
                    .Add<Pipelines.Blocks.ConditionDetailsView_BasicStringCompareBlock>()
                    .After<Pipelines.Blocks.ConditionDetailsView_PaymentBlock>())

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<Sitecore.Commerce.Plugin.Rules.IBuildRuleSetPipeline>(configure => configure
                    .Remove<Sitecore.Commerce.Plugin.Rules.BuildRuleSetBlock>()
                    .Add<Pipelines.Blocks.BuildRuleSetBlock>())

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<Sitecore.Commerce.EntityViews.IFormatEntityViewPipeline>(configure => configure
                    .Add<Pipelines.Blocks.PrettifySelectOptionsBlock>()
                    .After<Sitecore.Commerce.EntityViews.HighlightLocalizableViewPropertiesBlock>())


                ////////////////////////////////////////////////////////////////////////////////////////////////////

                .ConfigurePipeline<IRunningPluginsPipeline>(c => c.Add<RegisteredPluginBlock>())
            );
        }
    }
}