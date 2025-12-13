-- =====================================================
-- EXTENSIONS + COMMON TRIGGER
-- =====================================================
CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE OR REPLACE FUNCTION set_updated_on()
RETURNS trigger AS $$
BEGIN
  NEW.updated_on := now();
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- =====================================================
-- SCHEMAS (SUPABASE SAFE)
-- =====================================================
CREATE SCHEMA IF NOT EXISTS iam;
CREATE SCHEMA IF NOT EXISTS org;
CREATE SCHEMA IF NOT EXISTS patient;
CREATE SCHEMA IF NOT EXISTS clinical;
CREATE SCHEMA IF NOT EXISTS billing;
CREATE SCHEMA IF NOT EXISTS audit;

-- =====================================================
-- IAM SERVICE (RBAC – APP LEVEL, NOT SUPABASE AUTH)
-- =====================================================
CREATE TABLE iam.users (
  id uuid PRIMARY KEY, -- MUST MATCH auth.users.id (Supabase)
  tenant_id uuid NOT NULL,
  email text NOT NULL,
  full_name text NOT NULL,
  status text DEFAULT 'active',
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE UNIQUE INDEX ux_iam_users_email
ON iam.users (tenant_id, email);

CREATE TRIGGER trg_iam_users_u
BEFORE UPDATE ON iam.users
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

CREATE TABLE iam.roles (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  name text NOT NULL, -- ADMIN, DOCTOR, NURSE, CASHIER
  description text,
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE UNIQUE INDEX ux_iam_roles_name
ON iam.roles (tenant_id, name);

CREATE TRIGGER trg_iam_roles_u
BEFORE UPDATE ON iam.roles
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

CREATE TABLE iam.permissions (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  code text UNIQUE NOT NULL, -- patient.read, billing.write
  description text
);

CREATE TABLE iam.user_roles (
  user_id uuid NOT NULL, -- iam.users.id
  role_id uuid NOT NULL, -- iam.roles.id
  tenant_id uuid NOT NULL,
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  PRIMARY KEY (user_id, role_id)
);

CREATE TABLE iam.role_permissions (
  role_id uuid NOT NULL,
  permission_id uuid NOT NULL,
  tenant_id uuid NOT NULL,
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  PRIMARY KEY (role_id, permission_id)
);

-- =====================================================
-- ORG SERVICE
-- =====================================================
CREATE TABLE org.tenants (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_code text UNIQUE NOT NULL,
  name text NOT NULL,
  status text DEFAULT 'active',
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE TABLE org.branches (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  branch_code text NOT NULL,
  name text NOT NULL,
  city text,
  status text DEFAULT 'active',
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE UNIQUE INDEX ux_org_branch_code
ON org.branches (tenant_id, branch_code);

CREATE TRIGGER trg_org_branches_u
BEFORE UPDATE ON org.branches
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

CREATE TABLE org.facilities (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  branch_id uuid NOT NULL,
  name text NOT NULL,
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE TRIGGER trg_org_facilities_u
BEFORE UPDATE ON org.facilities
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

CREATE TABLE org.departments (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  facility_id uuid NOT NULL,
  name text NOT NULL,
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE TRIGGER trg_org_departments_u
BEFORE UPDATE ON org.departments
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

-- =====================================================
-- PATIENT SERVICE
-- =====================================================
CREATE TABLE patient.patients (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  branch_id uuid,
  mrn text NOT NULL,
  first_name text NOT NULL,
  last_name text,
  phone text,
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE UNIQUE INDEX ux_patient_mrn
ON patient.patients (tenant_id, mrn);

CREATE TRIGGER trg_patient_patients_u
BEFORE UPDATE ON patient.patients
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

-- =====================================================
-- CLINICAL SERVICE
-- =====================================================
CREATE TABLE clinical.staff (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  branch_id uuid NOT NULL,
  user_id uuid, -- iam.users.id
  staff_type text NOT NULL,
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE TRIGGER trg_clinical_staff_u
BEFORE UPDATE ON clinical.staff
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

CREATE TABLE clinical.encounters (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  branch_id uuid NOT NULL,
  patient_id uuid NOT NULL,
  encounter_type text NOT NULL,
  status text DEFAULT 'open',
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE INDEX idx_clinical_encounter_patient
ON clinical.encounters (tenant_id, patient_id);

CREATE TRIGGER trg_clinical_encounters_u
BEFORE UPDATE ON clinical.encounters
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

CREATE TABLE clinical.appointments (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  branch_id uuid NOT NULL,
  patient_id uuid NOT NULL,
  staff_id uuid NOT NULL,
  start_time timestamptz NOT NULL,
  end_time timestamptz NOT NULL,
  status text DEFAULT 'booked',
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE INDEX idx_clinical_appointments_staff
ON clinical.appointments (tenant_id, staff_id, start_time);

CREATE TRIGGER trg_clinical_appointments_u
BEFORE UPDATE ON clinical.appointments
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

-- =====================================================
-- BILLING SERVICE
-- =====================================================
CREATE TABLE billing.service_catalog (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  code text NOT NULL,
  name text NOT NULL,
  category text,
  price numeric(12,2),
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now()
);

CREATE TABLE billing.invoices (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  branch_id uuid NOT NULL,
  patient_id uuid NOT NULL,
  total_amount numeric(12,2) DEFAULT 0,
  status text DEFAULT 'draft',
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE INDEX idx_billing_invoice_patient
ON billing.invoices (tenant_id, patient_id);

CREATE TRIGGER trg_billing_invoices_u
BEFORE UPDATE ON billing.invoices
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

CREATE TABLE billing.invoice_items (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  invoice_id uuid NOT NULL,
  service_id uuid NOT NULL,
  quantity int DEFAULT 1,
  price numeric(12,2),
  amount numeric(12,2)
);

CREATE TABLE billing.payments (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  branch_id uuid NOT NULL,
  invoice_id uuid NOT NULL,
  amount numeric(12,2) NOT NULL,
  method text NOT NULL,
  is_deleted boolean DEFAULT false,
  created_by uuid NOT NULL,
  created_on timestamptz DEFAULT now(),
  updated_by uuid,
  updated_on timestamptz
);

CREATE TRIGGER trg_billing_payments_u
BEFORE UPDATE ON billing.payments
FOR EACH ROW EXECUTE FUNCTION set_updated_on();

-- =====================================================
-- AUDIT SERVICE
-- =====================================================
CREATE TABLE audit.audit_log (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid NOT NULL,
  actor_user_id uuid NOT NULL,
  action text NOT NULL,
  entity text NOT NULL,
  entity_id uuid,
  before_data jsonb,
  after_data jsonb,
  created_on timestamptz DEFAULT now(),
  created_by uuid NOT NULL,
  updated_by uuid,
  updated_on timestamptz
);

CREATE INDEX idx_audit_time
ON audit.audit_log (tenant_id, created_on DESC);

CREATE TRIGGER trg_audit_log_u
BEFORE UPDATE ON audit.audit_log
FOR EACH ROW EXECUTE FUNCTION set_updated_on();
