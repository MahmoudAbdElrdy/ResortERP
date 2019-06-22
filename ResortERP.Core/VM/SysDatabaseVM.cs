using System;

namespace ResortERP.Core.VM
{
    public class SysDatabaseVM
    {
        public string name { get; set; }
        public int database_id { get; set; }
        public Nullable<int> source_database_id { get; set; }
        public byte[] owner_sid { get; set; }
        public System.DateTime create_date { get; set; }
        public byte compatibility_level { get; set; }
        public string collation_name { get; set; }
        public Nullable<byte> user_access { get; set; }
        public string user_access_desc { get; set; }
        public Nullable<bool> is_read_only { get; set; }
        public bool is_auto_close_on { get; set; }
        public Nullable<bool> is_auto_shrink_on { get; set; }
        public Nullable<byte> state { get; set; }
        public string state_desc { get; set; }
        public Nullable<bool> is_in_standby { get; set; }
        public Nullable<bool> is_cleanly_shutdown { get; set; }
        public Nullable<bool> is_supplemental_logging_enabled { get; set; }
        public Nullable<byte> snapshot_isolation_state { get; set; }
        public string snapshot_isolation_state_desc { get; set; }
        public Nullable<bool> is_read_committed_snapshot_on { get; set; }
        public Nullable<byte> recovery_model { get; set; }
        public string recovery_model_desc { get; set; }
        public Nullable<byte> page_verify_option { get; set; }
        public string page_verify_option_desc { get; set; }
        public Nullable<bool> is_auto_create_stats_on { get; set; }
        public Nullable<bool> is_auto_create_stats_incremental_on { get; set; }
        public Nullable<bool> is_auto_update_stats_on { get; set; }
        public Nullable<bool> is_auto_update_stats_async_on { get; set; }
        public Nullable<bool> is_ansi_null_default_on { get; set; }
        public Nullable<bool> is_ansi_nulls_on { get; set; }
        public Nullable<bool> is_ansi_padding_on { get; set; }
        public Nullable<bool> is_ansi_warnings_on { get; set; }
        public Nullable<bool> is_arithabort_on { get; set; }
        public Nullable<bool> is_concat_null_yields_null_on { get; set; }
        public Nullable<bool> is_numeric_roundabort_on { get; set; }
        public Nullable<bool> is_quoted_identifier_on { get; set; }
        public Nullable<bool> is_recursive_triggers_on { get; set; }
        public Nullable<bool> is_cursor_close_on_commit_on { get; set; }
        public Nullable<bool> is_local_cursor_default { get; set; }
        public Nullable<bool> is_fulltext_enabled { get; set; }
        public Nullable<bool> is_trustworthy_on { get; set; }
        public Nullable<bool> is_db_chaining_on { get; set; }
        public Nullable<bool> is_parameterization_forced { get; set; }
        public bool is_master_key_encrypted_by_server { get; set; }
        public Nullable<bool> is_query_store_on { get; set; }
        public bool is_published { get; set; }
        public bool is_subscribed { get; set; }
        public bool is_merge_published { get; set; }
        public bool is_distributor { get; set; }
        public bool is_sync_with_backup { get; set; }
        public System.Guid service_broker_guid { get; set; }
        public bool is_broker_enabled { get; set; }
        public Nullable<byte> log_reuse_wait { get; set; }
        public string log_reuse_wait_desc { get; set; }
        public bool is_date_correlation_on { get; set; }
        public bool is_cdc_enabled { get; set; }
        public Nullable<bool> is_encrypted { get; set; }
        public Nullable<bool> is_honor_broker_priority_on { get; set; }
        public Nullable<System.Guid> replica_id { get; set; }
        public Nullable<System.Guid> group_database_id { get; set; }
        public Nullable<int> resource_pool_id { get; set; }
        public Nullable<short> default_language_lcid { get; set; }
        public string default_language_name { get; set; }
        public Nullable<int> default_fulltext_language_lcid { get; set; }
        public string default_fulltext_language_name { get; set; }
        public Nullable<bool> is_nested_triggers_on { get; set; }
        public Nullable<bool> is_transform_noise_words_on { get; set; }
        public Nullable<short> two_digit_year_cutoff { get; set; }
        public Nullable<byte> containment { get; set; }
        public string containment_desc { get; set; }
        public Nullable<int> target_recovery_time_in_seconds { get; set; }
        public Nullable<int> delayed_durability { get; set; }
        public string delayed_durability_desc { get; set; }
        public Nullable<bool> is_memory_optimized_elevate_to_snapshot_on { get; set; }
    }
}
