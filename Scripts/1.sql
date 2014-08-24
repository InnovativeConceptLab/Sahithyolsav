
ALTER TABLE tbl_item
ADD isGroupItem bit  
DEFAULT 0

alter table tbl_item
ADD intMaxNumberOfParticpant int
DEFAULT 1

alter table tbl_item
ADD intMarkForFirstPlace int
DEFAULT 0

alter table tbl_item
ADD intMarkForSecondPlace int
DEFAULT 0

alter table tbl_item
ADD intMarkForThirdPlace int
DEFAULT 0