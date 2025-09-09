export type Label = {
  id: number;
  name: string;
  color: string;
  description: string;
  projectId: number;
  createdOn: string;
};

export type LabelCreate = {
  name: string;
  color: string;
  description: string;
  projectId: number;
};

export type LabelUpdate = {
  name: string;
  color: string;
  description: string;
};