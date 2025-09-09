export type ProjectGet = {
  id: number;
  key: string;
  title: string;
  description: string;
};

export type ProjectPost = {
  key: string;
  title: string;
  description: string;
};

export type ProjectUpdate = {
  title: string;
  description: string;
};
